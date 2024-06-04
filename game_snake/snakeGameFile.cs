using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

public class SnakeGame : Form
{
    private List<Point> Snake = new List<Point>(); // C'est ta liste de points qui représente le serpent
    private Point Food = new Point(); // Ça, c'est le point où la bouffe va apparaître
    private Random rand = new Random(); // Le Random pour générer des trucs au hasard
    private int score = 0; // Le score, bien sûr
    private int dirX, dirY; // Direction du serpent
    private bool isPaused = true; // Pour savoir si le jeu est en pause ou pas

    // Les contrôles
    private Timer gameTimer = new Timer(); // Un timer pour gérer les mises à jour du jeu
    private PictureBox gameCanvas = new PictureBox(); // C'est là que le jeu est dessiné
    private Label lblScore = new Label(); // Label pour afficher le score
    private ComboBox cbDifficulty; // ComboBox pour choisir la difficulté
    private ComboBox cbMode; // ComboBox pour choisir le mode de jeu
    private Button btnStartPause; // Bouton pour démarrer ou mettre en pause le jeu

    public SnakeGame()
    {
        // Réglages du formulaire
        Text = "Snake Game"; // Nom du jeu
        Size = new Size(800, 600); // Taille du formulaire

        // Label du score
        lblScore.Text = "Score: 0"; // Affichage initial du score
        lblScore.Location = new Point(10, 10); // Position du label sur le formulaire
        Controls.Add(lblScore); // Ajout du label aux contrôles du formulaire

        // ComboBox pour la difficulté
        cbDifficulty = new ComboBox // Initialisation
        {
            Items = { "Facile", "Moyen", "Difficile" }, // Les choix de difficulté
            Location = new Point(10, 40), // Position sur le formulaire
            DropDownStyle = ComboBoxStyle.DropDownList, // On veut une liste déroulante
            SelectedIndex = 0 // Difficulté par défaut
        };
        Controls.Add(cbDifficulty); // Ajout au formulaire

        // ComboBox pour le mode de jeu
        cbMode = new ComboBox // Initialisation
        {
            Items = { "Classique", "Moderne" }, // Les choix de mode
            Location = new Point(10, 70), // Position sur le formulaire
            DropDownStyle = ComboBoxStyle.DropDownList, // On veut une liste déroulante
            SelectedIndex = 0 // Mode par défaut
        };
        Controls.Add(cbMode); // Ajout au formulaire

        // Bouton pour démarrer ou mettre en pause
        btnStartPause = new Button // Initialisation
        {
            Text = "Démarrer", // Texte du bouton
            Location = new Point(10, 100) // Position sur le formulaire
        };
        btnStartPause.Click += (sender, e) => // Événement click du bouton
        {
            if (isPaused) // Si le jeu est en pause
            {
                isPaused = false; // On met en pause
                btnStartPause.Text = "Pause"; // On change le texte du bouton
                gameTimer.Start(); // On démarre le timer
            }
            else // Si le jeu est en cours
            {
                isPaused = true; // On met en pause
                btnStartPause.Text = "Démarrer"; // On change le texte du bouton
                gameTimer.Stop(); // On arrête le timer
            }

            cbDifficulty.Enabled = isPaused; // On active/désactive la sélection de la difficulté
            cbMode.Enabled = isPaused; // On active/désactive la sélection du mode
        };
        Controls.Add(btnStartPause); // Ajout au formulaire

        // Le canvas du jeu
        gameCanvas.Size = new Size(600, 400); // Taille du canvas
        gameCanvas.Location = new Point(100, 100); // Position sur le formulaire
        gameCanvas.BackColor = Color.Black; // Couleur de fond
        gameCanvas.Paint += gameCanvas_Paint; // Événement pour dessiner sur le canvas
        Controls.Add(gameCanvas); // Ajout au formulaire

        // Le timer du jeu
        gameTimer.Interval = 1000; // Ça va être changé selon la difficulté
        gameTimer.Tick += Update; // Événement à chaque tick du timer
        gameTimer.Stop(); // On ne démarre pas tout de suite

        // Nouvelle partie
        NewGame(); // Fonction pour commencer une nouvelle partie
    }

    private void NewGame()
    {
        // Réinitialiser les paramètres
        Snake.Clear(); // Effacer le serpent
        Snake.Add(new Point(10, 10)); // Position initiale du serpent
        dirX = 1; // Direction initiale
        dirY = 0; // Direction initiale
        score = 0; // Score à zéro
        lblScore.Text = "Score: " + score.ToString(); // Mettre à jour le score

        // Placer la bouffe
        PlaceFood(); // Fonction pour placer la nourriture

        // Mettre à jour la difficulté
        UpdateDifficulty(); // Fonction pour mettre à jour la difficulté
    }

    private void UpdateDifficulty()
    {
        switch (cbDifficulty.SelectedItem.ToString())
        {
            case "Facile": gameTimer.Interval = 200; break; // Temps entre chaque mise à jour pour le mode facile
            case "Moyen": gameTimer.Interval = 100; break; // Temps entre chaque mise à jour pour le mode moyen
            case "Difficile": gameTimer.Interval = 50; break; // Temps entre chaque mise à jour pour le mode difficile
        }
    }

    private void PlaceFood()
    {
        Food = new Point(rand.Next(gameCanvas.Width / 10), rand.Next(gameCanvas.Height / 10)); // Position aléatoire pour la nourriture
    }

    private void Update(object sender, EventArgs e)
    {
        // Mettre à jour la difficulté
        UpdateDifficulty();

        // Vérifier si le serpent se cogne contre lui-même
        for (int i = 1; i < Snake.Count; i++)
            if (Snake[i].Equals(Snake[0])) EndGame();

        // Vérifier si le serpent mange la nourriture
        if (Snake[0].Equals(Food))
        {
            score++; // Augmenter le score
            lblScore.Text = "Score: " + score.ToString(); // Mettre à jour le score affiché
            // Ajouter au corps du serpent
            Snake.Add(new Point()); // Ajouter un point au serpent
            PlaceFood(); // Placer de la nourriture
        }

        // Vérifier si le serpent sort du cadre
        if (cbMode.SelectedItem.ToString() == "Classique")
        {
            if (Snake[0].X < 0 || Snake[0].Y < 0 || Snake[0].X >= gameCanvas.Width / 10 || Snake[0].Y >= gameCanvas.Height / 10)
                EndGame();
        }
        else
        {
            // Logique pour rebondir
            if (Snake[0].X < 0) Snake[0] = new Point(gameCanvas.Width / 10 - 1, Snake[0].Y);
            else if (Snake[0].X >= gameCanvas.Width / 10) Snake[0] = new Point(0, Snake[0].Y);
            else if (Snake[0].Y < 0) Snake[0] = new Point(Snake[0].X, gameCanvas.Height / 10 - 1);
            else if (Snake[0].Y >= gameCanvas.Height / 10) Snake[0] = new Point(Snake[0].X, 0);
        }

        // Bouger le serpent
        for (int i = Snake.Count - 1; i >= 0; i--)
        {
            if (i == 0)
                Snake[i] = new Point(Snake[i].X + dirX, Snake[i].Y + dirY);
            else
                Snake[i] = Snake[i - 1];
        }

        // Redessiner le jeu
        gameCanvas.Invalidate();
    }

    private void EndGame()
    {
        gameTimer.Stop(); // Arrêter le timer
        isPaused = true; // Mettre en pause
        btnStartPause.Text = "Démarrer"; // Changer le texte du bouton
        cbDifficulty.Enabled = true; // Activer la sélection de la difficulté
        cbMode.Enabled = true; // Activer la sélection du mode
        MessageBox.Show("Game Over"); // Message de fin de partie
        NewGame(); // Nouvelle partie
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (!isPaused)
        {
            switch (keyData)
            {
                case Keys.Up:
                    if (dirY != 1)
                    {
                        dirX = 0; dirY = -1;
                    }
                    break;
                case Keys.Down:
                    if (dirY != -1)
                    {
                        dirX = 0; dirY = 1;
                    }
                    break;
                case Keys.Left:
                    if (dirX != 1)
                    {
                        dirX = -1; dirY = 0;
                    }
                    break;
                case Keys.Right:
                    if (dirX != -1)
                    {
                        dirX = 1; dirY = 0;
                    }
                    break;
            }
        }

        return base.ProcessCmdKey(ref msg, keyData);
    }

    private void gameCanvas_Paint(object sender, PaintEventArgs e)
    {
        Graphics canvas = e.Graphics;

        for (int i = 0; i < Snake.Count; i++)
            canvas.FillRectangle(Brushes.Green, new Rectangle(Snake[i].X * 10, Snake[i].Y * 10, 10, 10));

        canvas.FillRectangle(Brushes.Red, new Rectangle(Food.X * 10, Food.Y * 10, 10, 10));
    }
}
