using com.democratia.Models;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using Xunit.Abstractions;

namespace com.democratia.Services
{
    internal class GroupClient : Client, IClient
    {
        public async Task<string> CreateModelAsync(params object?[]? parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetModelAsync(params object?[] parameters)
        {
            var requete = $"""
                ?request=SELECT g.id_groupe, nom_groupe, couleur_groupe, g.image, budget, nb_signalement, nbj_dft_discuss, nbj_dft_vote  FROM groupe g  INNER JOIN infos_membre ifo ON g.id_groupe = ifo.id_groupe WHERE ifo.id_internaute=?
                &parameters=["{((Internaute?)parameters![0])?.id_internaute}"]
                """;
            DebutRequete();
            HttpResponseMessage? response;
            try
            {
                response = await client!.GetAsync(requete);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Erreur de connexion inattendu", ex);
            }

            return await FinRequete(response);
            
        }

        internal async Task<ImageSource> GetImageAsync(string url)
        {
            var requete = $"""?request=obtenirImage&parameters=["{url}"]""";
            DebutRequete();
            HttpResponseMessage? response;
            try
            {
                response = await client!.GetAsync(requete);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Erreur de connexion inattendu", ex);
            }
            MettreAJourStatuts(response);
            if (!response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                throw new Exception("Requete râté");
            }

            else
            {
                // 1. On lit tout sous forme de tableau d'octets (byte[])
                byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();

                if (imageBytes == null || imageBytes.Length == 0)
                    return null;

                // 2. On retourne l'ImageSource
                // Chaque fois que MAUI en a besoin, il recrée un stream à partir du tableau en mémoire
                return ImageSource.FromStream(() => new MemoryStream(imageBytes));
            }

        }
    }
}
