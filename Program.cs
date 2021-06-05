using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace WordCounter
{
    public class Program
    {
        /* Deze applicatie is gemaakt door Guy Meuris op 1/03/2021 en telt het aantal 
         * woorden in een specifieke tekst 'Fingerprinting' en geeft deze weer in een rangschikking.
         * (Van meest voorkomend tot minder)
          */
        private static void Main()
        {
            // Pad opgeven naar tekstbestand
            var path = Path.Combine( System.AppDomain.CurrentDomain.BaseDirectory, "Fingerprinting.txt" );

            // Tekstbestand inlezen
            var text = File.ReadAllText(path).ToLower();

            // Controleren of het 'woord' enkel uit letters en/of cijfers bestaat en niets anders 
            var match = Regex.Match( text, "'?([a-zA-z'-]+)'?");

            // Combinatie 'woord' en 'aantal maal in tekst' gaan we opslaan in een dictionary,
            // dus deze moeten we eerst aanmaken
            Dictionary<string, int> freq = new Dictionary<string, int>();

            // Alle (door Regex gecontroleerde) woorden overlopen
            while ( match.Success )
            {
                string word = match.Value;
                if ( freq.ContainsKey( word ) )
                {
                    freq[word]++;    // aantal verhogen als het woord reeds in de dictionary staat
                }
                else
                {
                    freq.Add( word, 1 );  // als het een nieuw woord is
                }

                match = match.NextMatch();
            }

            // Alle woorden overlopen in een rangschikking (volgens aantal keer in de tekst)
            Console.WriteLine( "Rank  Word  Frequency" );
            Console.WriteLine( "====  ====  =========" );
            int rank = 1;

            // Aflopende volgorde aangeven
            foreach ( var elem in freq.OrderByDescending( a => a.Value ) )
            {
                Console.WriteLine( $"{rank++,2}    {elem.Key,-4}    {elem.Value,5}" );
            }
        }

    }
}
