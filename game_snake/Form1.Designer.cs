namespace Chatgptgame_snake // Ton espace de nom
{
    partial class Form1 // Ta classe partielle Form1
    {
        /// <summary>
        /// Variable de concepteur requise.
        /// </summary>
        private System.ComponentModel.IContainer components = null; // Les composants requis par le concepteur

        /// <summary>
        /// Libère les ressources non managées utilisées et éventuellement les ressources managées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing) // La méthode pour libérer les ressources
        {
            if (disposing && (components != null)) // Vérifier si les composants existent et si on doit libérer les ressources
            {
                components.Dispose(); // Libérer les composants
            }
            base.Dispose(disposing); // Appeler la méthode Dispose de la classe de base
        }

        #region Code généré par le concepteur de Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur. 
        /// Ne modifiez pas le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent() // Méthode pour initialiser les composants
        {
            this.SuspendLayout(); // Commencer la conception du formulaire
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F); // Taille de la police utilisée dans le formulaire
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; // Taille automatique basée sur la police
            this.ClientSize = new System.Drawing.Size(800, 450); // Taille du formulaire
            this.Name = "Form1"; // Nom du formulaire
            this.Text = "Form1"; // Texte du titre du formulaire
            this.ResumeLayout(false); // Fin de la conception du formulaire

        }

        
    }
}
