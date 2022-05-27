using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    internal class Position
    {

        private int position_num;//this is what the position is e.g 0, 1 etc

        private int [] relevant_conections=new int[8];

        private int num_of_cons = -1;

        public Position(int position_num)
        {
            this.position_num = position_num;
        }

        public int Position_num { get => position_num; set => position_num = value; }

        public int[] get_relevant_conections(CheckerType piece_color, bool is_king)
        {

            if (is_king == false)
            {
                switch (position_num)
                {
                    case 0:
                        {
                            if (piece_color == CheckerType.Red)
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 4;
                                relevant_conections[1] = 5;
                                relevant_conections[2] = -1;

                                relevant_conections[3] = 9;
                            }
                            


                            //check_relevant_spot_moves(ref can_move, ref can_jump, pos1, pos2, CheckerType.Black);

                        }
                        break;

                    case 1:
                        {
                            if (piece_color == CheckerType.Red)
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 5;
                                relevant_conections[1] = 6;
                                relevant_conections[2] = 8;
                                relevant_conections[3] = 10;
                            }
                          


                        }
                        break;

                    case 2:
                        {
                            if (piece_color == CheckerType.Red)
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 6;
                                relevant_conections[1] = 7;
                                relevant_conections[2] = 9;
                                relevant_conections[3] = 11;
                            }
                           


                        }
                        break;

                    case 3:
                        {
                            if (piece_color == CheckerType.Red)
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 7;
                                relevant_conections[1] = -1;
                                relevant_conections[2] = 10;
                                relevant_conections[3] = -1;

                            }



                        }
                        break;

                    case 4:
                        {
                            if (piece_color == CheckerType.Red)
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = -1;
                                relevant_conections[1] = 8;
                                relevant_conections[2] = -1;
                                relevant_conections[3] = 13;

                            }
                            else
                            {

                                relevant_conections[0] = -1;
                                relevant_conections[1] = 0;
                                relevant_conections[2] = -1;

                                relevant_conections[3] = -1;

                            }
                        }
                        break;

                    case 5:
                        {

                            if (piece_color == CheckerType.Red)
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 8;
                                relevant_conections[1] = 9;
                                relevant_conections[2] = 12;
                                relevant_conections[3] = 14;
                            }
                            else
                            {

                                relevant_conections[0] = 0;
                                relevant_conections[1] = 1;
                                relevant_conections[2] = -1;

                                relevant_conections[3] = -1;

                            }

                        }
                        break;
                    case 6:
                        {
                            if (piece_color == CheckerType.Red)
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 9;
                                relevant_conections[1] = 10;
                                relevant_conections[2] = 13;
                                relevant_conections[3] = 15;
                            }
                            else
                            {

                                relevant_conections[0] = 1;
                                relevant_conections[1] = 2;
                                relevant_conections[2] = -1;

                                relevant_conections[3] = -1;

                            }


                        }
                        break;
                    case 7:
                        {
                            if (piece_color == CheckerType.Red)
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 10;
                                relevant_conections[1] = 11;
                                relevant_conections[2] = 14;
                                relevant_conections[3] = -1;
                            }
                            else
                            {

                                relevant_conections[0] = 2;
                                relevant_conections[1] = 3;
                                relevant_conections[2] = -1;

                                relevant_conections[3] = -1;

                            }

                        }
                        break;
                    case 8:
                        {


                            if (piece_color == CheckerType.Red)
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 12;
                                relevant_conections[1] = 13;
                                relevant_conections[2] = -1;
                                relevant_conections[3] = 17;
                            }
                            else
                            {

                                relevant_conections[0] = 4;
                                relevant_conections[1] = 5;
                                relevant_conections[2] = -1;

                                relevant_conections[3] = 1;

                            }
                        }
                        break;

                    case 9:
                        {

                            if (piece_color == CheckerType.Red)
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 13;
                                relevant_conections[1] = 14;
                                relevant_conections[2] = 16;
                                relevant_conections[3] = 18;
                            }
                            else
                            {

                                relevant_conections[0] = 5;
                                relevant_conections[1] = 6;
                                relevant_conections[2] = 0;

                                relevant_conections[3] = 2;

                            }
                        }
                        break;
                    case 10:
                        {
                            if (piece_color == CheckerType.Red)
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 14;
                                relevant_conections[1] = 15;
                                relevant_conections[2] = 17;
                                relevant_conections[3] = 19;
                            }
                            else
                            {

                                relevant_conections[0] = 6;
                                relevant_conections[1] = 7;
                                relevant_conections[2] = 1;

                                relevant_conections[3] = 3;

                            }
                        }
                        break;

                    case 11:
                        {

                            if (piece_color == CheckerType.Red)
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 15;
                                relevant_conections[1] = -1;
                                relevant_conections[2] = 18;
                                relevant_conections[3] = -1;
                            }
                            else
                            {

                                relevant_conections[0] =  7;
                                relevant_conections[1] = -1;
                                relevant_conections[2] =  2;

                                relevant_conections[3] = -1;

                            }
                        }
                        break;
                    case 12:
                        {
                            if (piece_color == CheckerType.Red)
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = -1;
                                relevant_conections[1] = 16;
                                relevant_conections[2] = -1;
                                relevant_conections[3] = 21;
                            }
                            else
                            {

                                relevant_conections[0] = -1;
                                relevant_conections[1] = 8;
                                relevant_conections[2] = -1;

                                relevant_conections[3] = 5;

                            }

                        }
                        break;


                    case 13:
                        {

                            if (piece_color == CheckerType.Red)
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 16;
                                relevant_conections[1] = 17;
                                relevant_conections[2] = 20;
                                relevant_conections[3] = 22;
                            }
                            else
                            {

                                relevant_conections[0] = 8;
                                relevant_conections[1] = 9;
                                relevant_conections[2] = 4;

                                relevant_conections[3] = 6;

                            }
                        }
                        break;
                    case 14:
                        {
                            if (piece_color == CheckerType.Red)
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 17;
                                relevant_conections[1] = 18;
                                relevant_conections[2] = 21;
                                relevant_conections[3] = 23;
                            }
                            else
                            {

                                relevant_conections[0] = 9;
                                relevant_conections[1] = 10;
                                relevant_conections[2] = 5;

                                relevant_conections[3] = 7;

                            }
                        }
                        break;

                    case 15:
                        {

                            if (piece_color == CheckerType.Red)
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 18;
                                relevant_conections[1] = 19;
                                relevant_conections[2] = 22;
                                relevant_conections[3] = -1;
                            }
                            else
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 10;
                                relevant_conections[1] = 11;
                                relevant_conections[2] = 6;

                                relevant_conections[3] = -1;

                            }
                        }
                        break;
                    case 16:
                        {
                            if (piece_color == CheckerType.Red)
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 20;
                                relevant_conections[1] = 21;
                                relevant_conections[2] = -1;
                                relevant_conections[3] = 25;
                            }
                            else
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 12;
                                relevant_conections[1] = 13;
                                relevant_conections[2] = -1;

                                relevant_conections[3] = 9;

                            }
                        }
                        break;

                    case 17:
                        {

                            if (piece_color == CheckerType.Red)
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 21;
                                relevant_conections[1] = 22;
                                relevant_conections[2] = 24;
                                relevant_conections[3] = 26;
                            }
                            else
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 13;
                                relevant_conections[1] = 14;
                                relevant_conections[2] = 8;

                                relevant_conections[3] = 10;

                            }
                        }
                        break;
                    case 18:
                        {
                            if (piece_color == CheckerType.Red)
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 22;
                                relevant_conections[1] = 23;
                                relevant_conections[2] = 25;
                                relevant_conections[3] = 27;
                            }
                            else
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 14;
                                relevant_conections[1] = 15;
                                relevant_conections[2] = 9;

                                relevant_conections[3] = 11;

                            }
                        }
                        break;

                    case 19:
                        {
                            if (piece_color == CheckerType.Red)
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 23;
                                relevant_conections[1] = -1;
                                relevant_conections[2] = 26;
                                relevant_conections[3] = -1;
                            }
                            else
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 15;
                                relevant_conections[1] = -1;
                                relevant_conections[2] = 10;

                                relevant_conections[3] = -1;

                            }
                        }
                        break;

                    case 20:
                        {

                            if (piece_color == CheckerType.Red)
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = -1;
                                relevant_conections[1] = 24;
                                relevant_conections[2] = -1;
                                relevant_conections[3] = 29;
                            }
                            else
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = -1;
                                relevant_conections[1] = 16;
                                relevant_conections[2] = -1;

                                relevant_conections[3] = 13;

                            }

                        }
                        break;
                    case 21:
                        {
                            if (piece_color == CheckerType.Red)
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 24;
                                relevant_conections[1] = 25;
                                relevant_conections[2] = 28;
                                relevant_conections[3] = 30;
                            }
                            else
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 16;
                                relevant_conections[1] = 17;
                                relevant_conections[2] = 12;

                                relevant_conections[3] = 14;

                            }
                        }
                        break;
                    case 22:
                        {
                            if (piece_color == CheckerType.Red)
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 25;
                                relevant_conections[1] = 26;
                                relevant_conections[2] = 29;
                                relevant_conections[3] = 31;
                            }
                            else
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 17;
                                relevant_conections[1] = 18;
                                relevant_conections[2] = 13;

                                relevant_conections[3] = 15;

                            }
                        }
                        break;

                    case 23:
                        {


                            if (piece_color == CheckerType.Red)
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 26;
                                relevant_conections[1] = 27;
                                relevant_conections[2] = 30;
                                relevant_conections[3] = -1;
                            }
                            else
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 18;
                                relevant_conections[1] = 19;
                                relevant_conections[2] = 14;

                                relevant_conections[3] = -1;

                            }
                        }
                        break;

                    case 24:
                        {
                            if (piece_color == CheckerType.Red)
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 28;
                                relevant_conections[1] = 29;
                                relevant_conections[2] = -1;
                                relevant_conections[3] = -1;
                            }
                            else
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 20;
                                relevant_conections[1] = 21;
                                relevant_conections[2] = -1;

                                relevant_conections[3] = 17;

                            }
                        }
                        break;

                    case 25:
                        {


                            if (piece_color == CheckerType.Red)
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 29;
                                relevant_conections[1] = 30;
                                relevant_conections[2] = -1;
                                relevant_conections[3] = -1;
                            }
                            else
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 21;
                                relevant_conections[1] = 22;
                                relevant_conections[2] = 16;

                                relevant_conections[3] = 18;

                            }
                        }
                        break;

                    case 26:
                        {


                            if (piece_color == CheckerType.Red)
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 30;
                                relevant_conections[1] = 31;
                                relevant_conections[2] = -1;
                                relevant_conections[3] = -1;
                            }
                            else
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 22;
                                relevant_conections[1] = 23;
                                relevant_conections[2] = 17;

                                relevant_conections[3] = 19;

                            }
                        }
                        break;

                    case 27:
                        {


                            if (piece_color == CheckerType.Red)
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 31;
                                relevant_conections[1] = -1;
                                relevant_conections[2] = -1;
                                relevant_conections[3] = -1;
                            }
                            else
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 23;
                                relevant_conections[1] = -1;
                                relevant_conections[2] = 18;

                                relevant_conections[3] = -1;

                            }
                        }
                        break;

                    case 28:
                        {


                            if (piece_color == CheckerType.Black)
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = -1;
                                relevant_conections[1] = 24;
                                relevant_conections[2] = -1;
                                relevant_conections[3] = 21;
                            }
                            
                        }
                        break;

                    case 29:
                        {


                            if (piece_color == CheckerType.Black)
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 24;
                                relevant_conections[1] = 25;
                                relevant_conections[2] = 20;
                                relevant_conections[3] = 22;
                            }

                        }
                        break;

                    case 30:
                        {


                            if (piece_color == CheckerType.Black)
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 25;
                                relevant_conections[1] = 26;
                                relevant_conections[2] = 21;
                                relevant_conections[3] = 23;
                            }

                        }
                        break;

                    case 31:
                        {


                            if (piece_color == CheckerType.Black)
                            {
                                num_of_cons = 4;
                                relevant_conections[0] = 26;
                                relevant_conections[1] = 27;
                                relevant_conections[2] = 22;
                                relevant_conections[3] = -1;
                            }

                        }
                        break;











                    default:

                        break;
                }
            }
            else
            {
                switch (position_num)
                {
                    case 0:
                        {
                            
                            
                                num_of_cons = 8;
                                relevant_conections[0] = -1;
                                relevant_conections[1] = -1;
                                relevant_conections[2] = -1;

                                relevant_conections[3] = -1;

                                relevant_conections[4] = 4;
                                relevant_conections[5] = 5;
                                relevant_conections[6] = -1;

                                relevant_conections[7] = 9;
                            



                            //check_relevant_spot_moves(ref can_move, ref can_jump, pos1, pos2, CheckerType.Black);

                        }
                        break;

                    case 1:
                        {

                            num_of_cons = 8;
                            relevant_conections[0] = -1;
                            relevant_conections[1] = -1;
                            relevant_conections[2] = -1;

                            relevant_conections[3] = -1;

                            relevant_conections[4] = 5;
                            relevant_conections[5] = 6;
                            relevant_conections[6] = 8;

                            relevant_conections[7] = 10;



                        }
                        break;

                    case 2:
                        {

                            num_of_cons = 8;
                            relevant_conections[0] = -1;
                            relevant_conections[1] = -1;
                            relevant_conections[2] = -1;

                            relevant_conections[3] = -1;

                            relevant_conections[4] = 6;
                            relevant_conections[5] = 7;
                            relevant_conections[6] = 9;

                            relevant_conections[7] = 11;


                        }
                        break;

                    case 3:
                        {
                            
                            num_of_cons = 8;
                            relevant_conections[0] = -1;
                            relevant_conections[1] = -1;
                            relevant_conections[2] = -1;

                            relevant_conections[3] = -1;

                            relevant_conections[4] = 7;
                            relevant_conections[5] = -1;
                            relevant_conections[6] = 10;

                            relevant_conections[7] = -1;


                        }
                        break;

                    case 4:
                        {

                            num_of_cons = 8;
                            relevant_conections[0] = -1;
                            relevant_conections[1] = 0;
                            relevant_conections[2] = -1;

                            relevant_conections[3] = -1;

                            relevant_conections[4] = -1;
                            relevant_conections[5] = 8;
                            relevant_conections[6] = -1;

                            relevant_conections[7] = 13;
                        }
                        break;

                    case 5:
                        {


                            num_of_cons = 8;
                            relevant_conections[0] = 0;
                            relevant_conections[1] = 1;
                            relevant_conections[2] = -1;

                            relevant_conections[3] = -1;

                            relevant_conections[4] = 8;
                            relevant_conections[5] = 9;
                            relevant_conections[6] = 12;

                            relevant_conections[7] = 14;

                        }
                        break;
                    case 6:
                        {
                            num_of_cons = 8;
                            relevant_conections[0] = 1;
                            relevant_conections[1] = 2;
                            relevant_conections[2] = -1;

                            relevant_conections[3] = -1;

                            relevant_conections[4] = 9;
                            relevant_conections[5] = 10;
                            relevant_conections[6] = 13;

                            relevant_conections[7] = 15;


                        }
                        break;
                    case 7:
                        {

                            num_of_cons = 8;
                            relevant_conections[0] = 2;
                            relevant_conections[1] = 3;
                            relevant_conections[2] = -1;

                            relevant_conections[3] = -1;

                            relevant_conections[4] = 10;
                            relevant_conections[5] = 11;
                            relevant_conections[6] = 14;

                            relevant_conections[7] = -1;

                        }
                        break;
                    case 8:
                        {



                            num_of_cons = 8;
                            relevant_conections[0] = 4;
                            relevant_conections[1] = 5;
                            relevant_conections[2] = -1;

                            relevant_conections[3] = 1;

                            relevant_conections[4] = 12;
                            relevant_conections[5] = 13;
                            relevant_conections[6] = -1;

                            relevant_conections[7] = 17;
                        }
                        break;

                    case 9:
                        {


                            num_of_cons = 8;
                            relevant_conections[0] = 5;
                            relevant_conections[1] = 6;
                            relevant_conections[2] = 0;

                            relevant_conections[3] = 2;

                            relevant_conections[4] = 13;
                            relevant_conections[5] = 14;
                            relevant_conections[6] = 16;

                            relevant_conections[7] =18;
                        }
                        break;
                    case 10:
                        {

                            num_of_cons = 8;
                            relevant_conections[0] = 6;
                            relevant_conections[1] = 7;
                            relevant_conections[2] = 1;

                            relevant_conections[3] = 3;

                            relevant_conections[4] = 14;
                            relevant_conections[5] = 15;
                            relevant_conections[6] = 17;

                            relevant_conections[7] = 19;
                        }
                        break;

                    case 11:
                        {

                            num_of_cons = 8;
                            relevant_conections[0] = 7;
                            relevant_conections[1] = -1;
                            relevant_conections[2] = 2;

                            relevant_conections[3] = -1;

                            relevant_conections[4] = 15;
                            relevant_conections[5] = -1;
                            relevant_conections[6] = 18;

                            relevant_conections[7] = -1;
                        }
                        break;
                    case 12:
                        {

                            num_of_cons = 8;
                            relevant_conections[0] = -1;
                            relevant_conections[1] = 8;
                            relevant_conections[2] = -1;

                            relevant_conections[3] = 5;

                            relevant_conections[4] = -1;
                            relevant_conections[5] = 16;
                            relevant_conections[6] = -1;

                            relevant_conections[7] = 21;

                        }
                        break;


                    case 13:
                        {


                            num_of_cons = 8;
                            relevant_conections[0] = 8;
                            relevant_conections[1] = 9;
                            relevant_conections[2] = 4;

                            relevant_conections[3] = 6;

                            relevant_conections[4] = 16;
                            relevant_conections[5] = 17;
                            relevant_conections[6] = 20;

                            relevant_conections[7] = 22;
                        }
                        break;
                    case 14:
                        {

                            num_of_cons = 8;
                            relevant_conections[0] = 9;
                            relevant_conections[1] = 10;
                            relevant_conections[2] = 5;

                            relevant_conections[3] = 7;

                            relevant_conections[4] = 17;
                            relevant_conections[5] = 18;
                            relevant_conections[6] = 21;

                            relevant_conections[7] = 23;
                        }
                        break;

                    case 15:
                        {

                            num_of_cons = 8;
                            relevant_conections[0] = 10;
                            relevant_conections[1] = 11;
                            relevant_conections[2] = 6;

                            relevant_conections[3] = -1;

                            relevant_conections[4] = 18;
                            relevant_conections[5] = 19;
                            relevant_conections[6] = 22;

                            relevant_conections[7] = -1;
                        }
                        break;
                    case 16:
                        {

                            num_of_cons = 8;
                            relevant_conections[0] = 12;
                            relevant_conections[1] = 13;
                            relevant_conections[2] = -1;

                            relevant_conections[3] = 9;

                            relevant_conections[4] = 20;
                            relevant_conections[5] = 21;
                            relevant_conections[6] = -1;

                            relevant_conections[7] = 25;
                        }
                        break;

                    case 17:
                        {

                            num_of_cons = 8;
                            relevant_conections[0] = 13;
                            relevant_conections[1] = 14;
                            relevant_conections[2] = 8;

                            relevant_conections[3] = 10;

                            relevant_conections[4] = 21;
                            relevant_conections[5] = 22;
                            relevant_conections[6] = 24;

                            relevant_conections[7] = 26;
                        }
                        break;
                    case 18:
                        {

                            num_of_cons = 8;
                            relevant_conections[0] = 14;
                            relevant_conections[1] = 15;
                            relevant_conections[2] = 9;

                            relevant_conections[3] = 11;

                            relevant_conections[4] = 22;
                            relevant_conections[5] = 23;
                            relevant_conections[6] = 25;

                            relevant_conections[7] = 27;
                        }
                        break;

                    case 19:
                        {

                            num_of_cons = 8;
                            relevant_conections[0] = 15;
                            relevant_conections[1] = -1;
                            relevant_conections[2] = 10;

                            relevant_conections[3] = -1;

                            relevant_conections[4] = 23;
                            relevant_conections[5] = -1;
                            relevant_conections[6] = 26;

                            relevant_conections[7] = -1;
                        }
                        break;

                    case 20:
                        {


                            num_of_cons = 8;
                            relevant_conections[0] = -1;
                            relevant_conections[1] = 16;
                            relevant_conections[2] = -1;

                            relevant_conections[3] = 13;

                            relevant_conections[4] = -1;
                            relevant_conections[5] = 24;
                            relevant_conections[6] = -1;

                            relevant_conections[7] = 29;

                        }
                        break;
                    case 21:
                        {

                            num_of_cons = 8;
                            relevant_conections[0] =16;
                            relevant_conections[1] = 17;
                            relevant_conections[2] = 12;

                            relevant_conections[3] = 14;

                            relevant_conections[4] = 24;
                            relevant_conections[5] = 25;
                            relevant_conections[6] = 28;

                            relevant_conections[7] =30;
                        }
                        break;
                    case 22:
                        {

                            num_of_cons = 8;
                            relevant_conections[0] = 17;
                            relevant_conections[1] = 18;
                            relevant_conections[2] = 13;

                            relevant_conections[3] = 15;

                            relevant_conections[4] = 25;
                            relevant_conections[5] = 26;
                            relevant_conections[6] = 29;

                            relevant_conections[7] = 31;
                        }
                        break;

                    case 23:
                        {



                            num_of_cons = 8;
                            relevant_conections[0] = 18;
                            relevant_conections[1] = 19;
                            relevant_conections[2] = 14;

                            relevant_conections[3] = -1;

                            relevant_conections[4] = 26;
                            relevant_conections[5] = 27;
                            relevant_conections[6] = 30;

                            relevant_conections[7] = -1;
                        }
                        break;

                    case 24:
                        {

                            num_of_cons = 8;
                            relevant_conections[0] = 20;
                            relevant_conections[1] = 21;
                            relevant_conections[2] = -1;

                            relevant_conections[3] = 17;

                            relevant_conections[4] = 28;
                            relevant_conections[5] = 29;
                            relevant_conections[6] = -1;

                            relevant_conections[7] = -1;
                        }
                        break;

                    case 25:
                        {



                            num_of_cons = 8;
                            relevant_conections[0] = 21;
                            relevant_conections[1] = 22;
                            relevant_conections[2] = 16;

                            relevant_conections[3] = 18;

                            relevant_conections[4] = 29;
                            relevant_conections[5] = 30;
                            relevant_conections[6] = -1;

                            relevant_conections[7] = -1;
                        }
                        break;

                    case 26:
                        {



                            num_of_cons = 8;
                            relevant_conections[0] = 22;
                            relevant_conections[1] = 23;
                            relevant_conections[2] = 17;

                            relevant_conections[3] = 19;

                            relevant_conections[4] = 30;
                            relevant_conections[5] = 31;
                            relevant_conections[6] = -1;

                            relevant_conections[7] = -1;
                        }
                        break;

                    case 27:
                        {


                            num_of_cons = 8;
                            relevant_conections[0] = 23;
                            relevant_conections[1] = -1;
                            relevant_conections[2] = 18;

                            relevant_conections[3] = -1;

                            relevant_conections[4] = 31;
                            relevant_conections[5] = -1;
                            relevant_conections[6] = -1;

                            relevant_conections[7] = -1;
                        }
                        break;

                    case 28:
                        {


                            num_of_cons = 8;
                            relevant_conections[0] = -1;
                            relevant_conections[1] =24;
                            relevant_conections[2] = -1;

                            relevant_conections[3] = 21;

                            relevant_conections[4] = -1;
                            relevant_conections[5] = -1;
                            relevant_conections[6] = -1;

                            relevant_conections[7] = -1;

                        }
                        break;

                    case 29:
                        {



                            num_of_cons = 8;
                            relevant_conections[0] = 24;
                            relevant_conections[1] = 25;
                            relevant_conections[2] = 20;

                            relevant_conections[3] = 22;

                            relevant_conections[4] = -1;
                            relevant_conections[5] = -1;
                            relevant_conections[6] = -1;

                            relevant_conections[7] = -1;

                        }
                        break;

                    case 30:
                        {


                            num_of_cons = 8;
                            relevant_conections[0] = 25;
                            relevant_conections[1] = 26;
                            relevant_conections[2] = 21;

                            relevant_conections[3] = 23;

                            relevant_conections[4] = -1;
                            relevant_conections[5] = -1;
                            relevant_conections[6] = -1;

                            relevant_conections[7] = -1;

                        }
                        break;

                    case 31:
                        {


                            num_of_cons = 8;
                            relevant_conections[0] = 26;
                            relevant_conections[1] = 27;
                            relevant_conections[2] = 22;

                            relevant_conections[3] = -1;

                            relevant_conections[4] = -1;
                            relevant_conections[5] = -1;
                            relevant_conections[6] = -1;

                            relevant_conections[7] = -1;

                        }
                        break;











                    default:

                        break;
                }
            }


            return relevant_conections;
        }
    }
}
