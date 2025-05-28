using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;


namespace com.democratia.Utils
{
    /// <summary>
    /// Cette classe emballe un wrapper autour de la classe BackgroundWorker, permettant l'exécution facile de méthodes dans un thread séparé.
    /// Si vous souhaitez executer des fonctions sur l'UI thread sans soucis, implémentez onProgression et/ou onCancel et/ou onFinish selon le contexte
    /// La fonction en arrière plan quand à elle ne doit pas accéder à un quelconque élément de premier plan, au risque d'avoir une erreur de jetté
    /// par l'objet
    /// </summary>
    public sealed class WorkerThread
    {
        private readonly Delegate @delegate;
        private readonly Object?[]? parameters;
        private readonly Action<object>? onFinish;
        private readonly Action? onCanel;
        private readonly BackgroundWorker backgroundWorker;
        private readonly Action<object?, ProgressChangedEventArgs>? onProgression;


        

        /// <summary>
        /// Constructeur de la classe WorkerThread pour une fonction sans paramètres.
        /// </summary>
        /// <param name="delegate"> référence de la fonction a executer en arrière plan</param>
        /// <param name="cancellation">si vrai, le travail en arrière plan pourra être annulé via onCancel</param>
        /// <param name="onFinish">fonction à executer à la fin de la fonction en arrière qui prend en paramètre, le résultat de la fonction en arrière plan</param>
        /// <param name="onCancel">fonction a executer en cas d'annulation, sans paramètre et ne retourne rien</param>
        /// <param name="wantProgressBar">si vrai, la progression pourra être suivi via onProgression</param>
        /// <param name="onProgression">fonction à executer pendant la phase de progression, qui prend en paramètre l'appelant, un objet et l'événement, un ProgressChangedEventArgs</param>
        public WorkerThread(Delegate @delegate, Action<object>? onFinish, bool cancellation, Action? onCancel, bool wantProgressBar, Action<object?, ProgressChangedEventArgs>? onProgression)
        {
            this.@delegate = @delegate;
            this.onFinish = onFinish;
            if (cancellation && onCancel != null) this.onCanel = onCancel;
            if (wantProgressBar && onProgression != null) this.onProgression = onProgression;
            backgroundWorker = new()
            {
                WorkerSupportsCancellation = cancellation,
                WorkerReportsProgress = wantProgressBar
            };
            backgroundWorker.DoWork += BackgroundWorker_doWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_workerCompleted;
            if (backgroundWorker.WorkerReportsProgress) backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
        }

        /// <summary>
        /// Constructeur de la classe WorkerThread pour une fonction avec paramètres.
        /// </summary>
        /// <param name="delegate"> référence de la fonction a executer en arrière plan</param>
        /// <param name="cancellation">si vrai, le travail en arrière plan pourra être annulé via onCancel</param>
        /// <param name="onFinish">fonction à executer à la fin de la fonction en arrière qui prend en paramètre, le résultat de la fonction en arrière plan</param>
        /// <param name="onCancel">fonction a executer en cas d'annulation, sans paramètre et ne retourne rien</param>
        /// <param name="wantProgressBar">si vrai, la progression pourra être suivi via onProgression</param>
        /// <param name="onProgression">fonction à executer pendant la phase de progression, qui prend en paramètre l'appelant, un objet et l'événement, un ProgressChangedEventArgs</param>
        /// <param name="parameters">les paramètres de la fonction</param>
        public WorkerThread(Delegate @delegate, Action<object>? onFinish, bool cancellation, Action? onCancel, bool wantProgressBar, Action<object?, ProgressChangedEventArgs>? onProgression, params Object[] parameters)
            : this(@delegate, onFinish, cancellation, onCancel, wantProgressBar, onProgression) => this.parameters = parameters;

        /// <summary>
        /// Méthode pour executer la fonction en arrière plan.
        /// </summary>
        /// <exception cref="InvalidOperationException">Erreur jeté si le thread est déjà occupé</exception>
        public void Run()
        {
            if (backgroundWorker.IsBusy) throw new InvalidOperationException("Le thread est déjà occupé");
            backgroundWorker.RunWorkerAsync(parameters);
        }

        private void BackgroundWorker_ProgressChanged(object? sender, ProgressChangedEventArgs e) => onProgression?.Invoke(sender, e);
        
        private void BackgroundWorker_workerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                if (e.Error is COMException com) throw new InvalidOperationException("Il ne faut pas accéder au thread ui dans votre fonction, voici le message d'erreur complet " + com.Message);
                else throw e.Error;
            }
            else if (e.Cancelled) onCanel?.Invoke();
            else if (e.Result != null) onFinish?.Invoke(e.Result);
        }

        private void BackgroundWorker_doWork(object? sender, DoWorkEventArgs e)
        {
            if (sender != null && sender is BackgroundWorker worker)
            {
                if (worker.CancellationPending && worker.WorkerSupportsCancellation) e.Cancel = true;
                else try { e.Result = DynamicInvokeSmart(parameters); } catch (Exception ex) { throw ex ?? new Exception(""); }
            }
        }

        private dynamic? DynamicInvokeSmart(object?[]? flatArgs)
        {
            var parameters = @delegate.Method.GetParameters();

            if (parameters.Length == 0) try { return @delegate.DynamicInvoke(); } catch (TargetInvocationException targ) { throw targ.InnerException ?? targ; }

            var args = new object?[parameters.Length];

            bool hasParams = parameters.Last().IsDefined(typeof(ParamArrayAttribute), false);
            int fixedCount = hasParams ? parameters.Length - 1 : parameters.Length;

            // Vérifier qu'on a assez d'arguments
            if (flatArgs?.Length < fixedCount)
                throw new ArgumentException("Pas assez d’arguments pour les paramètres non-params");

            // Copier les arguments fixes
            for (int i = 0; i < fixedCount; i++)
                args[i] = flatArgs?[i];

            if (hasParams)
            {
                var paramElementType = parameters.Last().ParameterType.GetElementType()!;
                var paramArgs = flatArgs?.Skip(fixedCount).ToArray();

                var paramArray = paramArgs != null ? Array.CreateInstance(paramElementType, paramArgs.Length) : throw new NullReferenceException("référencement null");
                for (int i = 0; i < paramArgs.Length; i++)
                    paramArray.SetValue(paramArgs[i], i);

                args[parameters.Length - 1] = paramArray;
            }
            try { return @delegate.DynamicInvoke(args); } catch (TargetInvocationException targ) { throw targ.InnerException ?? targ; }
        }
    }
}
