using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlashCard
{
    
    public partial class Form2 : Form
    {
        private List<Card> cardsList = new List<Card>();
        private List<Card> choosenCards = new List<Card>();
        private int minimum;
        private int maximum;
        private int numberOfCards;
        private int currentCard = 0;
        private Boolean englishShowing = false;
        

        public Form2(List<Card> lc, int min, int max)
        {
            
            InitializeComponent();

            currentCard = 0;
            this.ActiveControl = label1;
            
            cardsList = lc;
            minimum = min - 1;
            maximum = max - 1;
            int count = minimum;
            numberOfCards = (max - min)+ 1;
            
            //choosenCards = cardsList.GetRange(min-1, max);

            for (int i = 0; i < numberOfCards; i++)
            {
                choosenCards.Add( lc[count]);
                count++;
            }
                Shuffle.ShuffleCards<Card>(choosenCards);

            
            txtDisplay.Text = choosenCards[0].greekWord;
            
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (currentCard == choosenCards.Count - 1)
            {
                currentCard = 0;
                txtDisplay.Text = choosenCards[currentCard].greekWord;
            }
            else
            {
                currentCard++;
                txtDisplay.Text = choosenCards[currentCard].greekWord;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (currentCard == 0)
            {
            }
            else
            {
                currentCard--;
                txtDisplay.Text = choosenCards[currentCard].greekWord;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (englishShowing == false)
            {
                txtDisplay.Text = choosenCards[currentCard].englishWord;
                englishShowing = true;
            }
            else
            {
                txtDisplay.Text = choosenCards[currentCard].greekWord;
                englishShowing = false;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
