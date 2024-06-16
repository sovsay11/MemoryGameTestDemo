namespace MemoryGameTestDemo
{
    public partial class MainPage : ContentPage
    {
        // Main variables
        // Determines which cards have been selected
        ImageButton firstSelection = new ImageButton();
        ImageButton secondSelection = new ImageButton();

        // Check when we have selected two cards
        int selectedCount = 0;

        // Keep track of the number of turns
        int numberOfTurns = 0;

        // Check whose turn it is
        bool playerOneTurn = true;

        // Keep track of the number of matches per player
        int playerOneMatches = 0;
        int playerTwoMatches = 0;

        // Image list, we add one item for each card
        List<string> cardList = new List<string>()
        {
            "eevee.png",
            "gardevoir.png",
            "gengar.png",
            "pikachu.png",
            "totodile.png",
            "toxapex.png",
            "typhlosion.png",
            "umbreon.png",
            "eevee.png",
            "gardevoir.png",
            "gengar.png",
            "pikachu.png",
            "totodile.png",
            "toxapex.png",
            "typhlosion.png",
            "umbreon.png"
        };

        // Copy of the image list above, but just shuffled
        List<string> shuffledCardList;

        // List of image button controls
        List<ImageButton> cardControls = new List<ImageButton>();

        /// <summary>
        /// Constructor for the page
        /// </summary>
        public MainPage()
        {
            // Defines the visual components of the UI
            InitializeComponent();

            cardControls.Add(Card1);
            cardControls.Add(Card2);
            cardControls.Add(Card3);
            cardControls.Add(Card4);
            cardControls.Add(Card5);
            cardControls.Add(Card6);
            cardControls.Add(Card7);
            cardControls.Add(Card8);
            cardControls.Add(Card9);
            cardControls.Add(Card10);
            cardControls.Add(Card11);
            cardControls.Add(Card12);
            cardControls.Add(Card13);
            cardControls.Add(Card14);
            cardControls.Add(Card15);
            cardControls.Add(Card16);

            // Shuffle the images
            Random random = new Random();
            shuffledCardList = cardList.OrderBy(card => random.Next()).ToList();
        }

        /// <summary>
        /// Logic for when a card is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void CardButton_Clicked(object sender, EventArgs e)
        {
            // First turn logic
            if (numberOfTurns == 0)
            {
                RadioButtonOnePlayer.IsEnabled = false;
                RadioButtonTwoPlayers.IsEnabled = false;
            }

            // Grab the card that was pressed
            ImageButton currentCard = (ImageButton)sender;

            // The pressed card doesn't have an image, assign one
            if (currentCard.Source == null)
            {
                currentCard.Source = shuffledCardList[int.Parse(currentCard.ClassId) - 1];
            }
            else
            {
                currentCard.Source = null;
            }

            // Check whether we're on the first or second card selection
            if (selectedCount == 0)
            {
                firstSelection = currentCard;
            }
            else if (selectedCount == 1)
            {
                secondSelection = currentCard;
            }

            selectedCount++;

            // If we have selected two cards to compare with each other
            if (selectedCount == 2)
            {
                // Pause and prevent the player from spam clicking for about 1 second
                ButtonShuffle.IsEnabled = false;
                GridCards.IsEnabled = false;
                await Task.Delay(1000);
                GridCards.IsEnabled = true;
                ButtonShuffle.IsEnabled = true;

                // Check if we have have a match
                if (firstSelection.Source?.ToString() == secondSelection.Source?.ToString())
                {
                    // If one player mode is on, only use one color
                    if (RadioButtonOnePlayer.IsChecked)
                    {
                        firstSelection.BackgroundColor = Color.FromArgb("f08e39");
                        secondSelection.BackgroundColor = Color.FromArgb("f08e39");
                    }
                    else if (RadioButtonTwoPlayers.IsChecked)
                    {
                        // Change the card background colors for the player, increment the player match count
                        if (playerOneTurn)
                        {
                            firstSelection.BackgroundColor = Color.FromArgb("f08e39");
                            secondSelection.BackgroundColor = Color.FromArgb("f08e39");
                            playerOneMatches++;
                            playerOneTurn = false;
                        }
                        else
                        {
                            firstSelection.BackgroundColor = Color.FromArgb("82c6f1");
                            secondSelection.BackgroundColor = Color.FromArgb("82c6f1");
                            playerTwoMatches++;
                            playerOneTurn = true;
                        }
                    }

                    // Disable the cards so you can't click on them again
                    firstSelection.IsEnabled = false;
                    secondSelection.IsEnabled = false;
                }
                // No match
                else
                {
                    // Hide the images again
                    firstSelection.Source = null;
                    secondSelection.Source = null;

                    // Alternate player turns
                    if (RadioButtonTwoPlayers.IsChecked)
                    {
                        if (playerOneTurn)
                        {
                            playerOneTurn = false;
                        }
                        else
                        {
                            playerOneTurn = true;
                        }
                    }
                }

                // Set selected card count back to 0, increment the number of turns, update the total turns label text
                selectedCount = 0;
                numberOfTurns++;
                LabelNumberOfTurns.Text = numberOfTurns.ToString();
            }

            // Check if we have matched everything
            if (playerOneMatches + playerTwoMatches == 8)
            {
                // Determine a winner
                if (playerOneMatches > playerTwoMatches)
                {
                    await DisplayAlert("Winner!", $"Player 1 won with {playerOneMatches} matches!", "Close");
                }
                else if (playerOneMatches < playerTwoMatches)
                {
                    await DisplayAlert("Winner!", $"Player 2 won with {playerTwoMatches} matches!", "Close");
                }
                else
                {
                    await DisplayAlert("Tie!", $"It was a tie!", "Close");
                }
            }
        }

        /// <summary>
        /// Resets all our variables and controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonShuffle_Clicked(object sender, EventArgs e)
        {
            // Reset all main variables
            firstSelection.Source = null;
            secondSelection.Source = null;
            RadioButtonOnePlayer.IsEnabled = true;
            RadioButtonTwoPlayers.IsEnabled = true;

            selectedCount = 0;
            numberOfTurns = 0;
            playerOneMatches = 0;
            playerTwoMatches = 0;
            LabelNumberOfTurns.Text = "0";

            playerOneTurn = true;

            // Shuffle the board again
            Random random = new Random();
            shuffledCardList = cardList.OrderBy(card => random.Next()).ToList();

            // Reset the cards in the UI
            foreach (var card in cardControls)
            {
                card.Source = null;
                card.BackgroundColor = Colors.White;
                card.IsEnabled = true;
            }
        }
    }
}
