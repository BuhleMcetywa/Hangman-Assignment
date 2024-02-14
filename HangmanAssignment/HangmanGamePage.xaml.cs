using static HangmanAssignment.HangmanGamePage;

namespace HangmanAssignment;

public partial class HangmanGamePage : ContentPage
{
        private List<string> secretWords = new List<string>() { "Sheldon", "Leonard", "Wolowitz", "Koothrapali", "Penny" };
    private string secretWord;
    private string guessedWord;
    private int turns;
    private int hangmanState;

    public HangmanGamePage()
    {
        InitializeComponent();
       
        ResetGame();

    }

    private void Guess_Clicked(object sender, EventArgs e)
    {
        string guess = Entry.Text.ToUpper();
        Entry.Text = "";

        bool isCorrect = false;
        for (int i = 0; i < secretWord.Length; i++)
        {

            if (secretWord[i] == ' ')
            {
                continue;
            }


            if (secretWord[i].ToString().ToUpper() == guess && guessedWord[i] == '_')
            {

                for (int j = 0; j < secretWord.Length; j++)
                {
                    if (secretWord[j].ToString().ToUpper() == guess && guessedWord[j] == '_')
                    {
                        guessedWord = guessedWord.Substring(0, j) + secretWord[j] + guessedWord.Substring(j + 1);
                    }
                }
                isCorrect = true;
            }
        }
        if (!isCorrect)
        {
            turns--;
            hangmanState++;
        }

        UpdateUI();

        if (guessedWord.Replace(" ", "").Equals(secretWord.Replace(" ", ""), StringComparison.OrdinalIgnoreCase))
        {
            DisplayAlert("Congratulations!", "You guessed correct, the word was " + guessedWord, "OK");
            ResetGame();
        }
        else if (turns == 0)
        {
            DisplayAlert("Sorry!", "You lost! The word was " + secretWord, "OK");
            ResetGame();
        }
    }

    private void UpdateUI()
    {

        string spacedGuessedWord = string.Join(" ", guessedWord.ToCharArray());

        WordLabel.Text = spacedGuessedWord;

        HangmanImage.Source = hangmanState switch
        {
            1 => (ImageSource)"hang1.png",
            2 => (ImageSource)"hang2.png",
            3 => (ImageSource)"hang3.png",
            4 => (ImageSource)"hang4.png",
            5 => (ImageSource)"hang5.png",
            6 => (ImageSource)"hang6.png",
            7 => (ImageSource)"hang7.png",
            8 => (ImageSource)"hang8.png",
            _ => null
        };

        if (turns > 0)
        {
            TurnsLabel.Text = "Turns remaining: " + turns;
        }
        else
        {
            TurnsLabel.Text = "Game Over!";
        }
    }

    private void ResetGame()
    {
        secretWord = secretWords[new Random().Next(secretWords.Count)];
        guessedWord = new string('_', secretWord.Length);
        turns = 7;
        hangmanState = 1;
        UpdateUI();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ResetGame();
    }
}

    

    