using System; // Pour les trucs de base
using System.Collections.Generic; // Pour les listes et tout
using System.Linq; // Pour manipuler les listes
using System.Threading.Tasks; // Pour les tâches asynchrones
using System.Windows.Forms; // Pour l'interface graphique

namespace Chatgptgame_snake // Ton espace de nom
{
    internal static class Program // Ta classe de programme
    {
        /// <summary>
        /// Le point d'entrée principal de l'application.
        /// </summary>
        [STAThread] // Un attribut pour dire que ça tourne en mode STA (Single Thread Apartment)
        static void Main() // Ta fonction principale
        {
            Application.EnableVisualStyles(); // Activer les styles visuels
            Application.SetCompatibleTextRenderingDefault(false); // Paramètres par défaut pour le rendu du texte
            Application.Run(new SnakeGame()); // Lancer ton jeu de serpent
        }
    }
}
