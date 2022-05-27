using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checkers
{
    enum CheckerType { Empty,Red,RedKing,Black,BlackKing};
    public partial class Checkers : Form
    {
        //the game board
        private CheckerBox[,] chkbxBoard;
        Heuristic[] heuristic_arr;
        //the three numbers storing the current board state
        private uint Red;
        private uint Black;
        private uint Kings;

        //Images to use representing the board and pieces
        private Image lightSquare;
        private Image darkSquare;
        private Image redChecker;
        private Image redCheckerSelected;
        private Image redKing;
        private Image redKingSelected;
        private Image blackChecker;
        private Image blackCheckerSelected;
        private Image blackKing;
        private Image blackKingSelected;

        //null if no checker is selected. otherwise references the CheckerBox of the selected checker
        private CheckerBox chkSelectedChecker;
        
        //If setup mode is true, clicking on a board square will cycle through the 
        //available pieces that can be placed on that square.
        private bool setupMode;

        //If midJump is true, the human player is in the middle of a multi-part jump. 
        //The selected piece cannot be changed.
        private bool midJump;

        public Checkers()
        {
            InitializeComponent();

            lightSquare = Image.FromFile("images\\lightSquare.png");
            darkSquare = Image.FromFile("images\\darkSquare.png");
            redChecker = Image.FromFile("images\\redChecker.png");
            redCheckerSelected = Image.FromFile("images\\redCheckerSelected.png");
            redKing = Image.FromFile("images\\redKing.png");
            redKingSelected = Image.FromFile("images\\redKingSelected.png");
            blackChecker = Image.FromFile("images\\blackChecker.png");
            blackCheckerSelected = Image.FromFile("images\\blackCheckerSelected.png");
            blackKing = Image.FromFile("images\\blackKing.png");
            blackKingSelected = Image.FromFile("images\\blackKingSelected.png");

            chkbxBoard = new CheckerBox[8, 8];
            chkSelectedChecker = null;
            Red = Black = Kings = 0;

            setupMode = false;
            midJump = false;

            tlpBoard.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            int panelColor = 0;
            for (int row=0;row<8;row++)
            {
                for (int col=0;col<8;col++)
                {
                    chkbxBoard[row,col] = new CheckerBox();
                    chkbxBoard[row, col].BorderStyle = BorderStyle.FixedSingle;
                    chkbxBoard[row, col].Padding = new Padding(0);
                    chkbxBoard[row, col].Margin = new Padding(0);
                    chkbxBoard[row, col].Dock = DockStyle.Fill;
                    chkbxBoard[row, col].SizeMode = PictureBoxSizeMode.StretchImage;

                    chkbxBoard[row, col].Row = row;
                    chkbxBoard[row, col].Col = col;

                    if (panelColor % 2 == 0)
                    {
                        chkbxBoard[row, col].Image = darkSquare;
                        chkbxBoard[row, col].Checker = CheckerType.Empty;
                        //We never click a dark square
                    }
                    else
                    {
                        chkbxBoard[row, col].Image = lightSquare;
                        chkbxBoard[row, col].Checker = CheckerType.Empty;
                        chkbxBoard[row, col].Click += CheckerBox_Click;

                    }



                    this.tlpBoard.Controls.Add(chkbxBoard[row, col], col, row);
                    panelColor += 1;

                }
                panelColor += 1;
            }
        }

        //Exit is chosen from the menu
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Force the computer to take a turn
        private void forceComputerTurnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ComputerMove();
        }

        //switch into or out of setup mode
        private void setupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (setupMode)
            {
                newToolStripMenuItem.Enabled = true;
                loadToolStripMenuItem.Enabled = true;
                forceComputerTurnToolStripMenuItem.Enabled = true;
                setupMode = false;
            }
            else
            {
                newToolStripMenuItem.Enabled = false;
                loadToolStripMenuItem.Enabled = false;
                forceComputerTurnToolStripMenuItem.Enabled = false;
                setupMode = true;
            }

        }

        //start a new game with the initial setup values
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Red = 4095;
            Black = 4293918720;
            Kings = 0;
            midJump = false;

            drawBoard(Red, Black, Kings);
        }

        //load a specific set of numbers for red, black, king values into the game
        //calls a custom dialog to do this
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadValues dlgLV = new LoadValues();
            dlgLV.Red = Red;
            dlgLV.Black = Black;
            dlgLV.Kings = Kings;

            if (dlgLV.ShowDialog() == DialogResult.OK)
            {
                Red = dlgLV.Red;
                Black = dlgLV.Black;
                Kings = dlgLV.Kings;

                drawBoard(Red, Black, Kings);
            }
        }

        //what happens when one of the checkerboxes that can hold a piece is clicked.
        private void CheckerBox_Click(object sender, EventArgs e)
        {
            CheckerBox curSquare = sender as CheckerBox;

            //Spit out to the console what we are clicking on
            switch (curSquare.Checker)
            {
                case CheckerType.Empty:
                    Console.WriteLine("Row: " + curSquare.Row.ToString() + " Col: " + curSquare.Col.ToString() + " Empty");
                    break;
                case CheckerType.Red:
                    Console.WriteLine("Row: " + curSquare.Row.ToString() + " Col: " + curSquare.Col.ToString() + " Red");
                    break;
                case CheckerType.RedKing:
                    Console.WriteLine("Row: " + curSquare.Row.ToString() + " Col: " + curSquare.Col.ToString() + " Red King");
                    break;
                case CheckerType.Black:
                    Console.WriteLine("Row: " + curSquare.Row.ToString() + " Col: " + curSquare.Col.ToString() + " Black");
                    break;
                case CheckerType.BlackKing:
                    Console.WriteLine("Row: " + curSquare.Row.ToString() + " Col: " + curSquare.Col.ToString() + " Black King");
                    break;
            }

            //Setup mode allows you to put checkers whereever you want on the board
            //Use this to construct specific situations.
            if (setupMode) {
                //clear the selected checker.
                //there's a good chance running setup will mess that up.
                chkSelectedChecker = null;

                //clear mid jump
                //there's a good chance running setup will mess this up too.
                midJump = false;

                switch (curSquare.Checker)
                {
                    case CheckerType.Empty:
                        curSquare.Checker = CheckerType.Red;
                        break;
                    case CheckerType.Red:
                        curSquare.Checker = CheckerType.RedKing;
                        break;
                    case CheckerType.RedKing:
                        curSquare.Checker = CheckerType.Black;
                        break;
                    case CheckerType.Black:
                        curSquare.Checker = CheckerType.BlackKing;
                        break;
                    case CheckerType.BlackKing:
                        curSquare.Checker = CheckerType.Empty;
                        break;
                }

                updateRBK();
                drawBoard(Red, Black, Kings);
                return;
            }

            if (chkSelectedChecker == curSquare && !midJump)
            {
                chkSelectedChecker = null;
                drawBoard(Red, Black, Kings);
            }
                
            else if (chkSelectedChecker == null)
            {
                //No Checker is selected. Make sure this is our checker.
                if (curSquare.Checker==CheckerType.Black || curSquare.Checker==CheckerType.BlackKing)
                {
                    chkSelectedChecker = curSquare;
                    drawBoard(Red, Black, Kings);
                }
                    
                //Else No Checker is selected and this isn't our checker. Ignore.
            }                
            else
            {
                //Selected Checker is set and we've clicked on a different square.
                //This is an attempt at a move
                if (MakeMove(curSquare))
                {
                    //The move succeeded. It's now the computer's turn
                    //Set the selected checker to null and draw the new board first.
                     ComputerMove();
                }
                //Else this wasn't a valid move (or is an unfinished multi-part move). Don't do anything.
            }
        }



        private void Checkers_ResizeEnd(object sender, EventArgs e)
        {
            int diff = this.Width - this.Height;

            if (diff < 77)
                this.Height = this.Width ;
            else if (diff > 77)
                this.Width = this.Height;

            tlpBoard.ResumeLayout();
            this.Refresh();

        }

        private void drawBoard(uint redCheckers,uint blackCheckers, uint kings)
        {
            int shiftBits = 0;
            int curPos = 0;
            for (int row=0;row<8;row++)
            {
                for (int col=0;col<8;col++)
                {
                    if (curPos%2 == 1)
                    {                       
                        bool isRed = Convert.ToBoolean(redCheckers >> shiftBits & 1);
                        bool isBlack = Convert.ToBoolean(blackCheckers >> shiftBits & 1);

                        if (isRed)
                        {
                            if ((kings>>shiftBits & 1) == 1)
                            {
                                if (chkSelectedChecker==chkbxBoard[row,col])
                                    chkbxBoard[row, col].Image = redKingSelected;
                                else
                                    chkbxBoard[row, col].Image = redKing;
                                chkbxBoard[row, col].Checker = CheckerType.RedKing;
                            }                                
                            else {
                                if (chkSelectedChecker == chkbxBoard[row, col])
                                    chkbxBoard[row, col].Image = redCheckerSelected;
                                else
                                    chkbxBoard[row, col].Image = redChecker;
                                chkbxBoard[row, col].Checker = CheckerType.Red;
                            }
                        }
                        else if (isBlack)
                        {
                            if ((kings >> shiftBits & 1) == 1)
                            {
                                if (chkSelectedChecker == chkbxBoard[row, col])
                                    chkbxBoard[row, col].Image = blackKingSelected;
                                else
                                    chkbxBoard[row, col].Image = blackKing;
                                chkbxBoard[row, col].Checker = CheckerType.BlackKing;
                            }
                            else
                            {
                                if (chkSelectedChecker == chkbxBoard[row, col])
                                    chkbxBoard[row, col].Image = blackCheckerSelected;
                                else
                                    chkbxBoard[row, col].Image = blackChecker;
                                chkbxBoard[row, col].Checker = CheckerType.Black;
                            }
                        }
                        else
                        { 
                            chkbxBoard[row, col].Image = lightSquare;
                            chkbxBoard[row, col].Checker = CheckerType.Empty;
                        }
                        shiftBits++;
                    }
                    curPos++;
                }
                curPos++;
               
            }
        }

        private void Checkers_ResizeBegin(object sender, EventArgs e)
        {
            tlpBoard.SuspendLayout();
        }

        private bool MakeMove(CheckerBox curSquare)
        {
            //curSquare is the second clicked square. Should be empty
            //chkSelectedChecker is the first clicked square. Should be a black checker
            
            //Make sure we've clicked a checker and an empty space
            if (curSquare.Checker == CheckerType.Empty && (chkSelectedChecker.Checker == CheckerType.Black || chkSelectedChecker.Checker == CheckerType.BlackKing))
            {

                //Moves for a Black Checker               
                if (curSquare.Row + 1 == chkSelectedChecker.Row && (curSquare.Col == chkSelectedChecker.Col - 1 || curSquare.Col == chkSelectedChecker.Col + 1) && !hasJumps())
                {
                    curSquare.Image = chkSelectedChecker.Image;

                    //Make the piece a king if moveing into the 0th row
                    if (curSquare.Row == 0)
                        curSquare.Checker = CheckerType.BlackKing;
                    else
                        curSquare.Checker = chkSelectedChecker.Checker;

                    chkSelectedChecker.Image = lightSquare;
                    chkSelectedChecker.Checker = CheckerType.Empty;
                    chkSelectedChecker = null;
                    updateRBK();
                    drawBoard(Red, Black, Kings);
                    return true;
               }

                //Additional moves for a Black King
                else if (curSquare.Row - 1 == chkSelectedChecker.Row && (curSquare.Col == chkSelectedChecker.Col - 1 || curSquare.Col == chkSelectedChecker.Col + 1) && chkSelectedChecker.Checker == CheckerType.BlackKing && !hasJumps())
                {
                    curSquare.Image = chkSelectedChecker.Image;
                    curSquare.Checker = chkSelectedChecker.Checker;
                    chkSelectedChecker.Image = lightSquare;
                    chkSelectedChecker.Checker = CheckerType.Empty;
                    chkSelectedChecker = null;
                    updateRBK();
                    drawBoard(Red, Black, Kings);
                    return true;
                }
                //Jumps for a Black Checker
                else if (curSquare.Row + 2 == chkSelectedChecker.Row && curSquare.Col + 2 == chkSelectedChecker.Col && (chkbxBoard[curSquare.Row+1,curSquare.Col+1].Checker == CheckerType.Red || chkbxBoard[curSquare.Row + 1, curSquare.Col + 1].Checker == CheckerType.RedKing))
                {
                    chkbxBoard[curSquare.Row + 1, curSquare.Col + 1].Checker = CheckerType.Empty;
                    bool newKing = false;

                    //Make the piece a king if jumping into the 0th row.
                    if (curSquare.Row == 0 && chkSelectedChecker.Checker == CheckerType.Black)
                    {
                        chkbxBoard[curSquare.Row, curSquare.Col].Checker = CheckerType.BlackKing;
                        newKing = true;
                    }                        
                    else
                        chkbxBoard[curSquare.Row, curSquare.Col].Checker = chkbxBoard[curSquare.Row + 2, curSquare.Col + 2].Checker;

                    chkbxBoard[curSquare.Row + 2, curSquare.Col + 2].Checker = CheckerType.Empty;
                    chkSelectedChecker = chkbxBoard[curSquare.Row, curSquare.Col];

                    //If there are no more jumps to make (or there are, but the piece is a new king)
                    //clear the selected checker
                    //redraw the board
                    //let the computer take a turn (return true)
                    if (!hasJumps(chkSelectedChecker) || newKing)
                    {
                        midJump = false;
                        chkSelectedChecker = null;
                        updateRBK();
                        drawBoard(Red, Black, Kings);
                        return true;
                    }
                    //there are jumps. redraw the board
                    //don't let the computer go.
                    else
                    {
                        midJump = true;
                        updateRBK();
                        drawBoard(Red, Black, Kings);
                        return false;
                    }
                }
                else if (curSquare.Row + 2 == chkSelectedChecker.Row && curSquare.Col - 2 == chkSelectedChecker.Col && (chkbxBoard[curSquare.Row + 1, curSquare.Col - 1].Checker == CheckerType.Red || chkbxBoard[curSquare.Row + 1, curSquare.Col - 1].Checker == CheckerType.RedKing))
                {
                    chkbxBoard[curSquare.Row + 1, curSquare.Col - 1].Checker = CheckerType.Empty;
                    bool newKing = false;

                    //Make the piece a king if jumping into the 0th row.
                    if (curSquare.Row == 0 && chkSelectedChecker.Checker == CheckerType.Black)
                    {
                        chkbxBoard[curSquare.Row, curSquare.Col].Checker = CheckerType.BlackKing;
                        newKing = true;
                    }                        
                    else
                        chkbxBoard[curSquare.Row, curSquare.Col].Checker = chkbxBoard[curSquare.Row + 2, curSquare.Col - 2].Checker;

                    chkbxBoard[curSquare.Row + 2, curSquare.Col - 2].Checker = CheckerType.Empty;
                    chkSelectedChecker = chkbxBoard[curSquare.Row, curSquare.Col];

                    //If there are no more jumps to make (or there are, but the piece is a new king)
                    //clear the selected checker
                    //redraw the board
                    //let the computer take a turn (return true)
                    if (!hasJumps(chkSelectedChecker) || newKing)
                    {
                        midJump = false;
                        chkSelectedChecker = null;
                        updateRBK();
                        drawBoard(Red, Black, Kings);
                        return true;
                    }
                    //there are jumps. redraw the board
                    //don't let the computer go.
                    else
                    {
                        midJump = true;
                        updateRBK();
                        drawBoard(Red, Black, Kings);
                        return false;
                    }
                }
                //Additional Jumps for a Black King
                else if (curSquare.Row - 2 == chkSelectedChecker.Row && curSquare.Col - 2 == chkSelectedChecker.Col && (chkbxBoard[curSquare.Row - 1, curSquare.Col - 1].Checker == CheckerType.Red || chkbxBoard[curSquare.Row - 1, curSquare.Col - 1].Checker == CheckerType.RedKing) && chkSelectedChecker.Checker == CheckerType.BlackKing)
                {
                    chkbxBoard[curSquare.Row - 1, curSquare.Col - 1].Checker = CheckerType.Empty;
                    chkbxBoard[curSquare.Row, curSquare.Col].Checker = chkbxBoard[curSquare.Row - 2, curSquare.Col - 2].Checker;
                    chkbxBoard[curSquare.Row - 2, curSquare.Col - 2].Checker = CheckerType.Empty;
                    chkSelectedChecker = chkbxBoard[curSquare.Row, curSquare.Col];

                    //If there are no more jumps to make
                    //clear the selected checker
                    //redraw the board
                    //let the computer take a turn (return true)
                    if (!hasJumps(chkSelectedChecker))
                    {
                        midJump = false;
                        chkSelectedChecker = null;
                        updateRBK();
                        drawBoard(Red, Black, Kings);
                        return true;
                    }
                    //there are jumps. redraw the board
                    //don't let the computer go.
                    else
                    {
                        midJump = true;
                        updateRBK();
                        drawBoard(Red, Black, Kings);
                        return false;
                    }
                }
                else if (curSquare.Row - 2 == chkSelectedChecker.Row && curSquare.Col + 2 == chkSelectedChecker.Col && (chkbxBoard[curSquare.Row - 1, curSquare.Col + 1].Checker == CheckerType.Red || chkbxBoard[curSquare.Row - 1, curSquare.Col + 1].Checker == CheckerType.RedKing) && chkSelectedChecker.Checker == CheckerType.BlackKing)
                {
                    chkbxBoard[curSquare.Row - 1, curSquare.Col + 1].Checker = CheckerType.Empty;
                    chkbxBoard[curSquare.Row, curSquare.Col].Checker = chkbxBoard[curSquare.Row - 2, curSquare.Col + 2].Checker;
                    chkbxBoard[curSquare.Row - 2, curSquare.Col + 2].Checker = CheckerType.Empty;
                    chkSelectedChecker = chkbxBoard[curSquare.Row, curSquare.Col];

                    //If there are no more jumps to make
                    //clear the selected checker
                    //redraw the board
                    //let the computer take a turn (return true)
                    if (!hasJumps(chkSelectedChecker))
                    {
                        midJump = false;
                        chkSelectedChecker = null;
                        updateRBK();
                        drawBoard(Red, Black, Kings);
                        return true;
                    }
                    //there are jumps. redraw the board
                    //don't let the computer go.
                    else
                    {
                        midJump = true;
                        updateRBK();
                        drawBoard(Red, Black, Kings);
                        return false;
                    }
                        
                }
            }  
            //no valid move was found
            return false;
        }

        //hasJumps has two parts.
        //part1: if there is a checker selected, report true if only that checker has jumps. Otherwise false.
        //part2: if no checker is selected, report true if any black checker has a jump. Otherwise false.
        private  bool hasJumps(CheckerBox curChecker = null)
        {
            //part 1
            if (curChecker != null)
            {
                //Could jump up.
                if (curChecker.Row>1)
                {
                    //Could jump up and left
                    if (curChecker.Col > 1 && (chkbxBoard[curChecker.Row - 1, curChecker.Col - 1].Checker == CheckerType.Red || chkbxBoard[curChecker.Row - 1, curChecker.Col - 1].Checker == CheckerType.RedKing) && chkbxBoard[curChecker.Row-2, curChecker.Col-2].Checker == CheckerType.Empty)
                    {
                        return true;
                    }

                    //Could jump up and right
                    if (curChecker.Col < 6 && (chkbxBoard[curChecker.Row - 1, curChecker.Col + 1].Checker == CheckerType.Red || chkbxBoard[curChecker.Row - 1, curChecker.Col + 1].Checker == CheckerType.RedKing) && chkbxBoard[curChecker.Row - 2, curChecker.Col + 2].Checker == CheckerType.Empty)
                    {
                        return true;
                    }
                }

                //Could jump down
                if (curChecker.Row < 6 && curChecker.Checker == CheckerType.BlackKing)
                {

                    //Could jump down and left
                    if (curChecker.Col > 1 && (chkbxBoard[curChecker.Row + 1, curChecker.Col - 1].Checker == CheckerType.Red || chkbxBoard[curChecker.Row + 1, curChecker.Col - 1].Checker == CheckerType.RedKing) && chkbxBoard[curChecker.Row + 2, curChecker.Col - 2].Checker == CheckerType.Empty)
                    {
                        return true;
                    }

                    //Could jump down and right
                    if (curChecker.Col < 6 && (chkbxBoard[curChecker.Row + 1, curChecker.Col + 1].Checker == CheckerType.Red || chkbxBoard[curChecker.Row + 1, curChecker.Col + 1].Checker == CheckerType.RedKing) && chkbxBoard[curChecker.Row + 2, curChecker.Col + 2].Checker == CheckerType.Empty)
                    {
                        return true;
                    }
                }
            }
            //part 2
            else
            {
                for (int row=0;row<8;row++)
                {
                    for (int col = 0; col < 8; col++)
                    {
                        if ((chkbxBoard[row, col].Checker == CheckerType.Black || chkbxBoard[row, col].Checker == CheckerType.BlackKing) && hasJumps(chkbxBoard[row, col]))
                            return true;
                    }
                }
            }

            return false;
        }

        //Updates the Red, Black and King values based on the current state of the pictureboxes on the board
        private void updateRBK()
        {
            Red = Black = Kings = 0;
            int curPos = 0;
            int bit = 0;

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (curPos % 2 == 1)
                    {
                        switch (chkbxBoard[row, col].Checker)
                        {
                            case CheckerType.Black:
                                Black = Black | (uint)(1 << bit);
                                break;
                            case CheckerType.BlackKing:
                                Black = Black | (uint)(1 << bit);
                                Kings = Kings | (uint)(1 << bit);
                                break;
                            case CheckerType.Red:
                                Red = Red | (uint)(1 << bit);
                                break;
                            case CheckerType.RedKing:
                                Red = Red | (uint)(1 << bit);
                                Kings = Kings | (uint)(1  << bit);
                                break;
                        }
                        bit++;
                    }

                    curPos++;
                }
                curPos++;
            }
        }

        void draw_poss_board(Boardstate board, int index, int player)
        {
            if(player==2)
                MessageBox.Show("drawing possible board: " + index+"\nplayer "+player+"(max)"+"\nboard value: "+board.calculate_heuristic());
            else
                MessageBox.Show("drawing possible board: " + index + "\nplayer " + player + "(min)" + "\nboard value: " + board.calculate_heuristic());

            drawBoard(board.Red, board.Black, board.Kings);
        }
        Heuristic find_best_move_helper(Boardstate[] possible_moves, int alpha, int beta ,int poss_mov_count, int how_deep_we_go, int player)
        {
            int best_move = 0;

            int best_heuristic_val = 0;

            CheckerType next_player_color;

            if (player == 2)
            {
                best_heuristic_val = int.MinValue;
            }
            else
            {
                best_heuristic_val = int.MaxValue;
            }

            CheckerType player_color = CheckerType.Red;

            Heuristic returned_heuristic=new Heuristic();

            returned_heuristic.Value = best_heuristic_val;

            int nextplayer = -1;

            if (player == 2) {//which player we look at moves for
                nextplayer = 1;
                next_player_color = CheckerType.Black;
            }
            else
            {
                nextplayer = 2;
                next_player_color = CheckerType.Red;
            }

            int best_index = 0;

            how_deep_we_go = how_deep_we_go - 1;



            if (how_deep_we_go == 0)
            {

                //MessageBox.Show("!!!New Branch Leaf!!!!");
                for (int i = 0; i < poss_mov_count; i++)
                {
                    try
                    {
                        if (possible_moves[i] != null)
                        {
                            //draw_poss_board(possible_moves[i], i, player);

                           

                            int compared_heuristic_val = possible_moves[i].calculate_heuristic();

                         

                            if (player == 2)
                            {
                               

                                if (compared_heuristic_val > best_heuristic_val)
                                {
                                    best_index = i;
                                    best_heuristic_val = compared_heuristic_val;
                                    returned_heuristic.Index = best_index;
                                    returned_heuristic.Value = best_heuristic_val;

                                }
                            }
                            else
                            {
                                if (compared_heuristic_val < best_heuristic_val)
                                {
                                    best_index = i;
                                    best_heuristic_val = compared_heuristic_val;
                                    returned_heuristic.Index = best_index;
                                    returned_heuristic.Value = best_heuristic_val;
                                }
                            }
                        }
                        else
                        {
                            i++;
                        }
                    }
                    catch (NullReferenceException err)
                    {
                        i++;
                    }
                    
                    

                }
                //MessageBox.Show("Best board is: " + returned_heuristic.Index+"\nvalue: "+ returned_heuristic.Value);
                return returned_heuristic;
            }
            else
            {
                for (int i = 0; i < poss_mov_count; i++)
                {


                    Boardstate checked_board = possible_moves[i];

                    


                    if (checked_board != null)
                    {

                        Boardstate win_check_board = new Boardstate(checked_board.Red, checked_board.Black, checked_board.Kings);
                        /*
                        if (win_check_board.Red == 0)
                        {
                            MessageBox.Show("case detected");
                        }
                        */
                        if (win_check_board.is_win(2))
                        {
                            if (player == 2)
                            {
                                returned_heuristic.Index = i;
                                returned_heuristic.Value = int.MaxValue;

                                return returned_heuristic;
                            }

                            
                        }
                        else 
                        {

                            win_check_board = null;
                            win_check_board = new Boardstate(checked_board.Red, checked_board.Black, checked_board.Kings); ;

                            if (win_check_board.is_win(1))
                            {
                                if (player == 1)
                                {
                                    returned_heuristic.Index = i;
                                    returned_heuristic.Value = int.MinValue;

                                    return returned_heuristic;
                                }

                            }

                            
                        }

                        
                    }
                    Boardstate[] new_possible_moves = new Boardstate[48];


                    Heuristic local_heuristic = new Heuristic();

                    local_heuristic.Index = i;

                    int all_moves = -1;

                    int local_best_move = -1;

                    try
                    {
                        if (checked_board != null)
                        {

                            new_possible_moves = checked_board.GetMoves(ref all_moves, next_player_color, checked_board.Black, checked_board.Red, checked_board.Kings);

                           


                            local_heuristic = find_best_move_helper(new_possible_moves, alpha, beta, all_moves, how_deep_we_go, nextplayer);

                            


                            if (player == 2)
                            {

                                //first do alpha beta prune, hopefuly prune happenes and we return
                                if (local_heuristic.Value >= beta)
                                {
                                    returned_heuristic.Index = i;
                                    returned_heuristic.Value = local_heuristic.Value;

                                    return returned_heuristic;
                                }

                                //more alpha beta prune, if prune does not happen updata alpha as needed

                                if (local_heuristic.Value > alpha)
                                {
                                    alpha = local_heuristic.Value; 
                                }

                                //end of alpha beta stuff

                                //MessageBox.Show("!!!Higher level comparison (max) !!!!nProposed: "+ local_heuristic.Value);
                                if (local_heuristic.Value > returned_heuristic.Value)
                                {
                                    returned_heuristic.Index = i;
                                    returned_heuristic.Value = local_heuristic.Value;
                                }

                            }
                            else
                            {
                                //MessageBox.Show("!!!Higher level comparison (min) !!!!nProposed: " + local_heuristic.Value);

                                //first do alpha beta prune, hopefuly prune happenes and we return
                                if (local_heuristic.Value <= alpha)
                                {
                                    returned_heuristic.Index = i;
                                    returned_heuristic.Value = local_heuristic.Value;

                                    return returned_heuristic;
                                }

                                //more alpha beta prune, if prune does not happen updata beta as needed

                                if (local_heuristic.Value < beta)
                                {
                                    beta = local_heuristic.Value;
                                }

                                //end of alpha beta stuff

                                if (local_heuristic.Value < returned_heuristic.Value)
                                {
                                    returned_heuristic.Index = i;
                                    returned_heuristic.Value = local_heuristic.Value;
                                }

                            }
                        }
                        else
                        {
                            i++;
                        }
                    }
                    catch (NullReferenceException err)
                    {
                        i++;
                    }
                }




                //MessageBox.Show("Best high level board is: " + returned_heuristic.Index + "\nvalue: " + returned_heuristic.Value);
                return returned_heuristic;
            }
        }
        int find_best_move(Boardstate [] possible_moves, int poss_mov_count)//this function recursively looks for best move
        {

          





            int best_move = 0;

            int how_deep_we_go = 10;

            int alpha = int.MinValue;//we're trying to do alpha beta pruning
            int beta = int.MaxValue;

            best_move = find_best_move_helper(possible_moves, alpha, beta, poss_mov_count, how_deep_we_go, 2).Index;//should return index of the best move



            return best_move;

        }


        int threaded_find_best_move(Boardstate[] possible_moves, int poss_mov_count)//this function recursively looks for best move
        {

            int best_move = -1;

            int best_heuristic_val = int.MinValue;
           
            heuristic_arr = new Heuristic[poss_mov_count];

            for (int curMove = 0; curMove < poss_mov_count; curMove++)
            {
                possible_moves[curMove].Array_pos = curMove;
            }

            Thread[] ThreadArr = new Thread[poss_mov_count];

            for (int curMove = 0; curMove < poss_mov_count; curMove++)
            {
                ThreadArr[curMove] = new Thread(new ParameterizedThreadStart(ThreadMove));
                ThreadArr[curMove].Start(possible_moves[curMove]);
            }


            for (int curThread = 0; curThread < poss_mov_count; curThread++)
            {
                ThreadArr[curThread].Join();
                if (heuristic_arr[curThread].Value > best_heuristic_val)
                {
                    best_heuristic_val = heuristic_arr[curThread].Value;
                    best_move = curThread;

                }
                ThreadArr[curThread] = null;
            }


            return best_move;
        }

        private void ThreadMove(object obj)
        {

            int alpha = int.MinValue;//we're trying to do alpha beta pruning
            int beta = int.MaxValue;

            int how_deep_we_go = 8;


            Boardstate curr_move =obj as Boardstate;


            Boardstate[] thread_local_pos_move = new Boardstate[48];

            int thread_move_count=-1;

            thread_local_pos_move = curr_move.GetMoves(ref thread_move_count, CheckerType.Black, curr_move.Black, curr_move.Red, curr_move.Kings);

            heuristic_arr[curr_move.Array_pos] = find_best_move_helper(thread_local_pos_move, alpha, beta, thread_move_count, how_deep_we_go, 1);


        }



            //Put your game tree here.
            //Use the values Red, Black and King to determine
            //Where the pieces are on the board currently.

            //Make sure to  assign your chosen values back to these numbers
            //and call drawBoard() when finished.
            private void ComputerMove()
        {
            //We'll discuss this more in class, but three unsigned integers store 
            //the position and type of all checkers. Uncomment the three lines below 
            //to see an extra red king show up on the board at position 12 when the human
            //makes a move. Position numbering starts with 0 in the upper-left corner

            //Bitwise manipulation of Red and Kings to add a red king at position 12.
            //Red = Red | (uint)(1 << 12);
            //Kings = Kings | (uint)(1 << 12);
            //drawBoard(Red,Black,Kings);

            //Additionally, you can uncomment the lines below to check if a red king exists at position 12
            //if (Convert.ToBoolean(Red >> 12 & 1) && Convert.ToBoolean(Kings >> 12 & 1))
            //{
            //    MessageBox.Show("Red King at 12");
            //}
            //else
            //{
            //    MessageBox.Show("No Red King at 12");
            //}
            Boardstate[] possible_moves = new Boardstate[48];

            int poss_mov_count = -1;
            int best_move = -1;

            Boardstate initial_state = new Boardstate(Red, Black, Kings);

            Boardstate win_board_check=new Boardstate(Red, Black, Kings);

            if (win_board_check.is_win(1))
            {

                MessageBox.Show("!!!The Magnificent AI Won!!!");

                return;
            }
         

            //MessageBox.Show("Board value" + initial_state.calculate_heuristic());

            //MessageBox.Show("Board value" + initial_state.calculate_heuristic());





            /*
           possible_moves = initial_state.GetMoves(ref poss_mov_count, CheckerType.Black, Black, Red, Kings);
           for (int i = 0; i < poss_mov_count; i++)
           {



               uint demo_red = possible_moves[i].Red;
               uint demo_black= possible_moves[i].Black;
               uint demo_kings = possible_moves[i].Kings;

               drawBoard(demo_red, demo_black, demo_kings);

               MessageBox.Show("Possible move " + i);
               //MessageBox.Show("Board value" + possible_moves[i].calculate_heuristic());

           }

            
           */

           possible_moves = initial_state.GetMoves(ref poss_mov_count, CheckerType.Red, Black, Red, Kings);
           best_move = threaded_find_best_move(possible_moves, poss_mov_count);

           uint try_red = possible_moves[best_move].Red;
           uint try_black = possible_moves[best_move].Black;
           uint try_kings = possible_moves[best_move].Kings;



           drawBoard(try_red, try_black, try_kings);

           this.Black = try_black;
           this.Kings = try_kings;
           this.Red = try_red;

            win_board_check = new Boardstate(Red, Black, Kings);

            if (win_board_check.is_win(2))
            {

                MessageBox.Show("!!!Player One Won!!!");

                return;
            }

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog1.FileName;
            string text = Red.ToString() + Environment.NewLine + Black.ToString() + Environment.NewLine + Kings.ToString() + Environment.NewLine;
            System.IO.File.WriteAllText(filename, text);
            MessageBox.Show("File is saved");
        }

        private void rulesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Правила поддавков:\r\n1.Доска 8х8 клеток;\r\n2.Шашки занимают первые три ряда с каждой стороны;\r\n3.Дамка может ходить в любую сторону на 1 поле;\r\n4.при бое простой шашкой через поле превращения она превращается в дамку и продолжает бой по правилам дамки;\r\n5.Бить можно произвольное количество шашек в любых направлениях;\r\n6.Цель игры - отдать все союзные шашки.");
        }

        private void Checkers_Load(object sender, EventArgs e)
        {

        }
    }
}
