using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checkers
{
    internal class Boardstate
    {
        private uint red;
        private uint black;
        private uint kings;
        private int win_board;
        private bool win_pre_calc;
        private int array_pos;//position in array of possible moves

        public Heuristic heuristic_val;

        //the basic idea is that every boardstate will be able to tell me all possible moves
        Boardstate[] possible_moves = new Boardstate[48];

        public Boardstate(uint red, uint black, uint kings)
        {
            this.red = red;
            this.black = black;
            this.kings = kings;
            this.win_board = 0;
            win_pre_calc = false;
        }

        public uint Red { get => red; set => red = value; }
        public uint Black { get => black; set => black = value; }
        public uint Kings { get => kings; set => kings = value; }
        public int Array_pos { get => array_pos; set => array_pos = value; }

        bool pos_has_black_king(int position)
        {
            bool is_occupied = false;

            if (Convert.ToBoolean(Black >> position & 1) && Convert.ToBoolean(Kings >> position & 1))
            {
                is_occupied = true;
            }

            return is_occupied;
        }

        bool pos_has_red_king(int position)
        {
            bool is_occupied = false;

            if (Convert.ToBoolean(Red >> position & 1) && Convert.ToBoolean(Kings >> position & 1))
            {
                is_occupied = true;
            }

            return is_occupied;
        }


        bool pos_has_red_piece(int position)
        {
            bool is_occupied = false;

            if (Convert.ToBoolean(Red >> position & 1))
            {
                is_occupied = true;
            }

            return is_occupied;
        }

        bool pos_has_black_piece(int position)
        {
            bool is_occupied = false;

            if (Convert.ToBoolean(Black >> position & 1))
            {
                is_occupied = true;
            }

            return is_occupied;
        }
      
        void check_relevant_spot_moves(ref bool can_move, ref bool can_jump, int pos1, int pos2, CheckerType enemy)//lets me check left move is possible
        {

            if (enemy == CheckerType.Black)
            {
                if (pos1 != -1 && pos_has_red_piece(pos1) == false && pos_has_black_piece(pos1) == false)
                {
                    can_move = true;
                }
                else if (pos2 != -1 && pos_has_red_piece(pos2) == false && pos_has_black_piece(pos2) == false)
                {
                    if (enemy == CheckerType.Black)//we need to check there is an enemy
                    {
                        if (pos_has_black_piece(pos1) == true)
                        {
                            can_jump = true;
                        }
                    }
                    else
                    {
                        if (pos_has_red_piece(pos1) == true)
                        {
                            can_jump = true;
                        }
                    }

                }
            }
            else if (enemy == CheckerType.Red)
            {
                if (pos1 != -1 && pos_has_black_piece(pos1) == false && pos_has_red_piece(pos1) == false)
                {
                    can_move = true;
                }
                else if (pos2 != -1 && pos_has_black_piece(pos2) == false && pos_has_red_piece(pos2) == false)
                {
                    if (enemy == CheckerType.Black)//we need to check there is an enemy
                    {
                        if (pos_has_black_piece(pos1) == true)
                        {
                            can_jump = true;
                        }
                    }
                    else
                    {
                        if (pos_has_red_piece(pos1) == true)
                        {
                            can_jump = true;
                        }
                    }

                }
            }
        }

        bool king_must_jump_right(int position)
        {
            bool must_jump = false;
            int from_pos = -1;
            int to_pos = -1;
            int enemypos = -1;
            if (king_can_move_right(position, ref from_pos, ref to_pos, ref enemypos) == 2)
            {
                must_jump = true;
            }

            return must_jump;
        }

        bool king_must_jump__back_right(int position)
        {
            bool must_jump = false;
            int from_pos = -1;
            int to_pos = -1;
            int enemypos = -1;
            if (king_can_move_back_right(position, ref from_pos, ref to_pos, ref enemypos) == 2)
            {
                must_jump = true;
            }

            return must_jump;
        }

        bool king_must_jump_left(int position)//tells us if piece must jump left
        {
            bool must_jump = false;
            int from_pos = -1;
            int to_pos = -1;
            int enemypos = -1;
            if (king_can_move_left(position, ref from_pos, ref to_pos, ref enemypos) == 2)
            {
                must_jump = true;
            }

            return must_jump;
        }

        bool king_must_jump__back_left(int position)
        {
            bool must_jump = false;
            int from_pos = -1;
            int to_pos = -1;
            int enemypos = -1;
            if (king_can_move_back_left(position, ref from_pos, ref to_pos, ref enemypos) == 2)
            {
                must_jump = true;
            }

            return must_jump;
        }

        bool black_piece_must_jump_right(int position)//tells us if red piece must jump right
        {
            bool must_jump = false;
            int from_pos = -1;
            int to_pos = -1;
            int enemypos = -1;
            if (black_piece_can_move_right(position, ref from_pos, ref to_pos, ref enemypos) == 2)
            {
                must_jump = true;
            }

            return must_jump;
        }


        bool red_piece_must_jump_right(int position)//tells us if red piece must jump right
        {
            bool must_jump = false;
            int from_pos = -1;
            int to_pos = -1;
            int enemypos = -1;
            if (red_piece_can_move_right(position, ref from_pos, ref to_pos, ref enemypos) == 2)
            {
                must_jump = true;
            }

            return must_jump;
        }

        bool black_piece_must_jump_left(int position)//tells us if piece must jump left
        {
            bool must_jump = false;
            int from_pos = -1;
            int to_pos = -1;
            int enemypos = -1;
            if (black_piece_can_move_left(position, ref from_pos, ref to_pos, ref enemypos) == 2)
            {
                must_jump = true;
            }

            return must_jump;
        }
        bool red_piece_must_jump_left(int position)//tells us if piece must jump left
        {
            bool must_jump = false;
            int from_pos = -1;
            int to_pos = -1;
            int enemypos = -1;
            if (red_piece_can_move_left(position, ref from_pos, ref to_pos, ref enemypos) == 2)
            {
                must_jump = true;
            }

            return must_jump;
        }

        int king_can_move_right(int position, ref int frompos, ref int topos, ref int enemy_pos)
        {

            bool can_move = false;
            bool can_jump = false;
            int return_val = 0;
            int[] relevant_positions = null;

            bool is_black_king = pos_has_black_king(position);//this will later be used to know it's a black king
            bool is_redking = pos_has_red_king(position);//this will later be used to know it's a red king

            if (is_black_king || is_redking)
            {
                //we use the switch statement to decide which case it is
                Position checked_position = new Position(position);

                
                relevant_positions = new int[8];

                if (is_black_king)
                {
                    relevant_positions = checked_position.get_relevant_conections(CheckerType.Black, is_black_king);
                }
                else
                {
                    relevant_positions = checked_position.get_relevant_conections(CheckerType.Red, is_redking);
                }

                int pos1 = relevant_positions[1];
                int pos2 = relevant_positions[3];
                frompos = position;
                if (is_black_king)
                {
                    check_relevant_spot_moves(ref can_move, ref can_jump, pos1, pos2, CheckerType.Red);
                }
                else
                {
                    check_relevant_spot_moves(ref can_move, ref can_jump, pos1, pos2, CheckerType.Black);
                }
                

            }

            if (can_move)
            {
                return_val = 1;
                topos = relevant_positions[1];
            }
            else if (can_jump)
            {
                return_val = 2;
                enemy_pos= relevant_positions[1];
                topos = relevant_positions[3];
            }

            return return_val;
        }


        int black_piece_can_move_right(int position, ref int frompos, ref int topos, ref int enemy_pos)
        {

            bool can_move = false;
            bool can_jump = false;
            int return_val = 0;
            int[] relevant_positions = null;

            if (pos_has_black_piece(position))
            {
                //we use the switch statement to decide which case it is
                Position checked_position = new Position(position);

                bool is_black_king = false;//this will later be used to know it's a red king

                relevant_positions = new int[8];

                relevant_positions = checked_position.get_relevant_conections(CheckerType.Black, is_black_king);

                if (is_black_king == false)//if it's not a king
                {
                    int pos1 = relevant_positions[1];
                    int pos2 = relevant_positions[3];
                    frompos = position;

                    check_relevant_spot_moves(ref can_move, ref can_jump, pos1, pos2, CheckerType.Red);
                }

            }

            if (can_move)
            {
                return_val = 1;
                topos = relevant_positions[1];
            }
            else if (can_jump)
            {
                return_val = 2;
                enemy_pos = relevant_positions[1]; ;
                topos = relevant_positions[3];
            }

            return return_val;
        }

        int king_can_move_back_right(int position, ref int frompos, ref int topos, ref int enemy_pos)
        {

            bool can_move = false;
            bool can_jump = false;
            int return_val = 0;
            int[] relevant_positions = null;

            bool is_black_king = pos_has_black_king(position);//this will later be used to know it's a black king
            bool is_redking = pos_has_red_king(position);//this will later be used to know it's a red king

            if (is_black_king || is_redking)
            {
                //we use the switch statement to decide which case it is
                Position checked_position = new Position(position);


                relevant_positions = new int[8];

                if (is_black_king)
                {
                    relevant_positions = checked_position.get_relevant_conections(CheckerType.Black, is_black_king);
                }
                else
                {
                    relevant_positions = checked_position.get_relevant_conections(CheckerType.Red, is_redking);
                }

                int pos1 = relevant_positions[5];
                int pos2 = relevant_positions[7];
                frompos = position;
                if (is_black_king)
                {
                    check_relevant_spot_moves(ref can_move, ref can_jump, pos1, pos2, CheckerType.Red);
                }
                else
                {
                    check_relevant_spot_moves(ref can_move, ref can_jump, pos1, pos2, CheckerType.Black);
                }


            }

            if (can_move)
            {
                return_val = 1;
                topos = relevant_positions[5];
            }
            else if (can_jump)
            {
                return_val = 2;
                enemy_pos= relevant_positions[5];
                topos = relevant_positions[7];
            }

            return return_val;
        }

        int red_piece_can_move_right(int position, ref int frompos, ref int topos, ref int enemy_pos)
        { //returns 1 if red can move right, 2 if can jump right

            bool can_move = false;
            bool can_jump = false;
            int return_val = 0;
            int[] relevant_positions = null;


            if (pos_has_red_piece(position))
            {
                //we use the switch statement to decide which case it is
                Position checked_position = new Position(position);

                bool is_red_king = false;//this will later be used to know it's a red king

                relevant_positions = new int[8];

                relevant_positions = checked_position.get_relevant_conections(CheckerType.Red, is_red_king);

                if (is_red_king == false)//if it's not a king
                {
                    int pos1 = relevant_positions[1];
                    int pos2 = relevant_positions[3];
                    check_relevant_spot_moves(ref can_move, ref can_jump, pos1, pos2, CheckerType.Black);
                }

            }

            if (can_move)
            {
                return_val = 1;
                topos = relevant_positions[1];
            }
            else if (can_jump)
            {
                return_val = 2;
                enemy_pos= relevant_positions[1];
                topos = relevant_positions[3];
            }

            return return_val;

        }

        int king_can_move_left(int position, ref int frompos, ref int topos, ref int enemy_pos)
        {

            bool can_move = false;
            bool can_jump = false;
            int return_val = 0;
            int[] relevant_positions = null;

            bool is_black_king = pos_has_black_king(position);//this will later be used to know it's a black king
            bool is_redking = pos_has_red_king(position);//this will later be used to know it's a red king

            if (is_black_king || is_redking)
            {
                //we use the switch statement to decide which case it is
                Position checked_position = new Position(position);


                relevant_positions = new int[8];

                if (is_black_king)
                {
                    relevant_positions = checked_position.get_relevant_conections(CheckerType.Black, is_black_king);
                }
                else
                {
                    relevant_positions = checked_position.get_relevant_conections(CheckerType.Red, is_redking);
                }

                int pos1 = relevant_positions[0];
                int pos2 = relevant_positions[2];
                frompos = position;
                if (is_black_king)
                {
                    check_relevant_spot_moves(ref can_move, ref can_jump, pos1, pos2, CheckerType.Red);
                }
                else
                {
                    check_relevant_spot_moves(ref can_move, ref can_jump, pos1, pos2, CheckerType.Black);
                }


            }

            if (can_move)
            {
                return_val = 1;
                topos = relevant_positions[0];
            }
            else if (can_jump)
            {
                return_val = 2;
                enemy_pos = relevant_positions[0];
                topos = relevant_positions[2];
            }

            return return_val;
        }

        int black_piece_can_move_left(int position, ref int frompos, ref int topos , ref int enemy_pos)
        {

            bool can_move = false;
            bool can_jump = false;
            int return_val = 0;
            int[] relevant_positions = null;

            if (pos_has_black_piece(position))
            {
                //we use the switch statement to decide which case it is
                Position checked_position = new Position(position);

                bool is_black_king = false;//this will later be used to know it's a red king

                relevant_positions = new int[8];

                relevant_positions = checked_position.get_relevant_conections(CheckerType.Black, is_black_king);

                if (is_black_king == false)//if it's not a king
                {
                    int pos1 = relevant_positions[0];
                    int pos2 = relevant_positions[2];
                    frompos = position;

                    check_relevant_spot_moves(ref can_move, ref can_jump, pos1, pos2, CheckerType.Red);
                }

            }

            if (can_move)
            {
                return_val = 1;
                topos = relevant_positions[0];
            }
            else if (can_jump)
            {
                return_val = 2;
                enemy_pos= relevant_positions[0];
                topos = relevant_positions[2];
            }

            return return_val;
        }

        int king_can_move_back_left(int position, ref int frompos, ref int topos, ref int enemy_pos)
        {

            bool can_move = false;
            bool can_jump = false;
            int return_val = 0;
            int[] relevant_positions = null;

            bool is_black_king = pos_has_black_king(position);//this will later be used to know it's a black king
            bool is_redking = pos_has_red_king(position);//this will later be used to know it's a red king

            if (is_black_king || is_redking)
            {
                //we use the switch statement to decide which case it is
                Position checked_position = new Position(position);


                relevant_positions = new int[8];

                if (is_black_king)
                {
                    relevant_positions = checked_position.get_relevant_conections(CheckerType.Black, is_black_king);
                }
                else
                {
                    relevant_positions = checked_position.get_relevant_conections(CheckerType.Red, is_redking);
                }

                int pos1 = relevant_positions[4];
                int pos2 = relevant_positions[6];
                frompos = position;
                if (is_black_king)
                {
                    check_relevant_spot_moves(ref can_move, ref can_jump, pos1, pos2, CheckerType.Red);
                }
                else
                {
                    check_relevant_spot_moves(ref can_move, ref can_jump, pos1, pos2, CheckerType.Black);
                }


            }

            if (can_move)
            {
                return_val = 1;
                topos = relevant_positions[4];
            }
            else if (can_jump)
            {
                return_val = 2;
                enemy_pos = relevant_positions[4];
                topos = relevant_positions[6];
            }

            return return_val;
        }
        int red_piece_can_move_left(int position, ref int frompos, ref int topos, ref int enemy_pos)//returns 1 if red can move left, 2 if can jump left
        {
            bool can_move = false;
            bool can_jump = false;
            int return_val = 0;
            int[] relevant_positions=null;

            if (pos_has_red_piece(position))
            {
                //we use the switch statement to decide which case it is
                Position checked_position = new Position(position);

                bool is_red_king = false;//this will later be used to know it's a red king

                relevant_positions=new int[8];

                relevant_positions = checked_position.get_relevant_conections(CheckerType.Red, is_red_king);

                if (is_red_king == false)//if it's not a king
                {
                    int pos1 = relevant_positions[0];
                    int pos2 = relevant_positions[2];
                    frompos = position;

                    check_relevant_spot_moves(ref can_move, ref can_jump, pos1, pos2, CheckerType.Black);

                 
                }

            }

            if (can_move)
            {
                return_val = 1;
                topos= relevant_positions[0];
            }
            else if (can_jump)
            {
                return_val = 2;
                enemy_pos = relevant_positions[0];
                topos = relevant_positions[2];
            }

            return return_val;

        }
        void move_black_piece(int from, int to)//this should move a red piece from 1 loc to another
        {

            if (pos_has_black_king(from))
            {
                this.black = this.black ^= (uint)(1 << from);
                this.kings = this.kings ^= (uint)(1 << from);
                this.black = this.black | (uint)(1 << to);
                this.kings = this.kings | (uint)(1 << to);
            }
            else
            {
                if (pos_has_black_piece(from))
                {
                    this.black = this.black ^= (uint)(1 << from);
                    this.black = this.black | (uint)(1 << to);
                    if (to == 0 || to == 1 || to == 2 || to == 3)
                    {
                        become_black_king(to);
                    }
                }
                else
                {
                    MessageBox.Show("Wrong piece color selected");
                }
            }
        }

        void become_black_king(int position)
        {
            if (pos_has_black_piece(position))
            {
                this.kings = this.kings | (uint)(1 << position);
            }
        }

        void become_red_king(int position)
        {
            if (pos_has_red_piece(position))
            {
                this.kings = this.kings | (uint)(1 << position);
            }
        }
        void move_red_piece(int from, int to)//this should move a red piece from 1 loc to another
        {

            if (pos_has_red_king(from))
            {
                this.red = this.red ^= (uint)(1 << from);
                this.kings = this.kings ^= (uint)(1 << from);
                this.red = this.red | (uint)(1 << to);
                this.kings = this.kings | (uint)(1 << to);

               
            }
            else
            {
                if (pos_has_red_piece(from))
                {
                    this.red = this.red ^= (uint)(1 << from);
                    this.red = this.red | (uint)(1 << to);

                    if (to == 28 || to == 29 || to == 30 || to == 31)
                    {
                        become_red_king(to);
                    }
                }
                else
                {
                    MessageBox.Show("Wrong piece color selected");
                }
            }

            
        }

        Boardstate check_xtra_red_jmps(int to_pos, Boardstate added_state, ref int move_count, Boardstate[] possible_moves, ref bool found_xtra)
        {
            int enemy_pos = -1;
            int from_pos = -1;
            int local_to_pos = -1;

            if (red_piece_can_move_left(to_pos, ref from_pos, ref local_to_pos, ref enemy_pos) != 2 && red_piece_can_move_right(to_pos, ref from_pos, ref local_to_pos, ref enemy_pos) != 2)
                return added_state;
            else
            {

                if (red_piece_can_move_left(to_pos, ref from_pos, ref local_to_pos, ref enemy_pos) == 2)
                {
                    found_xtra = true;
                    Boardstate new_left_board = new Boardstate(added_state.red, added_state.black, added_state.kings);

                    new_left_board.jump_black_piece(from_pos, local_to_pos, enemy_pos);

                    new_left_board = new_left_board.check_xtra_red_jmps(local_to_pos, new_left_board, ref move_count, possible_moves, ref found_xtra);



                    possible_moves[move_count - 1] = new_left_board;

                    move_count++;
                }


                if (red_piece_can_move_right(to_pos, ref from_pos, ref local_to_pos, ref enemy_pos) == 2)
                {
                    found_xtra = true;
                    Boardstate new_rite_board = new Boardstate(added_state.red, added_state.black, added_state.kings);
                    new_rite_board.jump_black_piece(from_pos, local_to_pos, enemy_pos);

                    new_rite_board = new_rite_board.check_xtra_red_jmps(local_to_pos, new_rite_board, ref move_count, possible_moves, ref found_xtra);



                    possible_moves[move_count - 1] = new_rite_board;

                    move_count++;
                }
            }

            return null;
        }

        Boardstate check_xtra_red_king_jmps(int to_pos, Boardstate added_state, ref int move_count, Boardstate[] possible_moves, ref bool found_xtra)
        {
            int enemy_pos = -1;
            int from_pos = -1;
            int local_to_pos = -1;

            if (king_can_move_left(to_pos, ref from_pos, ref local_to_pos, ref enemy_pos) != 2 && king_can_move_right(to_pos, ref from_pos, ref local_to_pos, ref enemy_pos) != 2
                && king_can_move_back_left(to_pos, ref from_pos, ref local_to_pos, ref enemy_pos) != 2 && king_can_move_back_right(to_pos, ref from_pos, ref local_to_pos, ref enemy_pos) != 2)
                return added_state;
            else
            {

                if (king_can_move_left(to_pos, ref from_pos, ref local_to_pos, ref enemy_pos) == 2)
                {
                    found_xtra = true;
                    Boardstate new_left_board = new Boardstate(added_state.red, added_state.black, added_state.kings);

                    new_left_board.jump_black_piece(from_pos, local_to_pos, enemy_pos);

                    new_left_board = new_left_board.check_xtra_red_king_jmps(local_to_pos, new_left_board, ref move_count, possible_moves, ref found_xtra);



                    possible_moves[move_count - 1] = new_left_board;

                    move_count++;
                }


                if (king_can_move_right(to_pos, ref from_pos, ref local_to_pos, ref enemy_pos) == 2)
                {
                    found_xtra = true;
                    Boardstate new_rite_board = new Boardstate(added_state.red, added_state.black, added_state.kings);
                    new_rite_board.jump_black_piece(from_pos, local_to_pos, enemy_pos);

                    new_rite_board = new_rite_board.check_xtra_red_king_jmps(local_to_pos, new_rite_board, ref move_count, possible_moves, ref found_xtra);



                    possible_moves[move_count - 1] = new_rite_board;

                    move_count++;
                }

                if (king_can_move_back_left(to_pos, ref from_pos, ref local_to_pos, ref enemy_pos) == 2)
                {
                    found_xtra = true;
                    Boardstate new_rite_board = new Boardstate(added_state.red, added_state.black, added_state.kings);
                    new_rite_board.jump_black_piece(from_pos, local_to_pos, enemy_pos);

                    new_rite_board = new_rite_board.check_xtra_red_king_jmps(local_to_pos, new_rite_board, ref move_count, possible_moves, ref found_xtra);



                    possible_moves[move_count - 1] = new_rite_board;

                    move_count++;
                }

                if (king_can_move_back_right(to_pos, ref from_pos, ref local_to_pos, ref enemy_pos) == 2)
                {
                    found_xtra = true;
                    Boardstate new_rite_board = new Boardstate(added_state.red, added_state.black, added_state.kings);
                    new_rite_board.jump_black_piece(from_pos, local_to_pos, enemy_pos);

                    new_rite_board = new_rite_board.check_xtra_red_king_jmps(local_to_pos, new_rite_board, ref move_count, possible_moves, ref found_xtra);



                    possible_moves[move_count - 1] = new_rite_board;

                    move_count++;
                }


            }

            return null;
        }
        Boardstate check_xtra_black_king_jmps(int to_pos, Boardstate added_state, ref int move_count, Boardstate[] possible_moves, ref bool found_xtra)
        {
            int enemy_pos = -1;
            int from_pos = -1;
            int local_to_pos = -1;

            if (king_can_move_left(to_pos, ref from_pos, ref local_to_pos, ref enemy_pos) != 2 && king_can_move_right(to_pos, ref from_pos, ref local_to_pos, ref enemy_pos) != 2
                && king_can_move_back_left(to_pos, ref from_pos, ref local_to_pos, ref enemy_pos)!=2 && king_can_move_back_right(to_pos, ref from_pos, ref local_to_pos, ref enemy_pos) !=2 )
                return added_state;
            else
            {

                if (king_can_move_left(to_pos, ref from_pos, ref local_to_pos, ref enemy_pos) == 2)
                {
                    found_xtra = true;
                    Boardstate new_left_board = new Boardstate(added_state.red, added_state.black, added_state.kings);

                    new_left_board.jump_red_piece(from_pos, local_to_pos, enemy_pos);

                    new_left_board = new_left_board.check_xtra_black_king_jmps(local_to_pos, new_left_board, ref move_count, possible_moves, ref found_xtra);



                    possible_moves[move_count - 1] = new_left_board;

                    move_count++;
                }


                if (king_can_move_right(to_pos, ref from_pos, ref local_to_pos, ref enemy_pos) == 2)
                {
                    found_xtra = true;
                    Boardstate new_rite_board = new Boardstate(added_state.red, added_state.black, added_state.kings);
                    new_rite_board.jump_red_piece(from_pos, local_to_pos, enemy_pos);

                    new_rite_board = new_rite_board.check_xtra_black_king_jmps(local_to_pos, new_rite_board, ref move_count, possible_moves, ref found_xtra);



                    possible_moves[move_count - 1] = new_rite_board;

                    move_count++;
                }

                if (king_can_move_back_left(to_pos, ref from_pos, ref local_to_pos, ref enemy_pos) == 2)
                {
                    found_xtra = true;
                    Boardstate new_rite_board = new Boardstate(added_state.red, added_state.black, added_state.kings);
                    new_rite_board.jump_red_piece(from_pos, local_to_pos, enemy_pos);

                    new_rite_board = new_rite_board.check_xtra_black_king_jmps(local_to_pos, new_rite_board, ref move_count, possible_moves, ref found_xtra);



                    possible_moves[move_count - 1] = new_rite_board;

                    move_count++;
                }

                if (king_can_move_back_right(to_pos, ref from_pos, ref local_to_pos, ref enemy_pos) == 2)
                {
                    found_xtra = true;
                    Boardstate new_rite_board = new Boardstate(added_state.red, added_state.black, added_state.kings);
                    new_rite_board.jump_red_piece(from_pos, local_to_pos, enemy_pos);

                    new_rite_board = new_rite_board.check_xtra_black_king_jmps(local_to_pos, new_rite_board, ref move_count, possible_moves, ref found_xtra);



                    possible_moves[move_count - 1] = new_rite_board;

                    move_count++;
                }

              
            }

            return null;
        }
        Boardstate check_xtra_black_jmps(int to_pos, Boardstate added_state, ref int move_count, Boardstate[] possible_moves, ref bool found_xtra)
        {
            int enemy_pos = -1;
            int from_pos = -1;
            int local_to_pos = -1;

            if (black_piece_can_move_left(to_pos, ref from_pos, ref local_to_pos, ref enemy_pos) != 2 && black_piece_can_move_right(to_pos, ref from_pos, ref local_to_pos, ref enemy_pos) != 2)
                return added_state;
            else
            {

                if (black_piece_can_move_left(to_pos, ref from_pos, ref local_to_pos, ref enemy_pos) == 2)
                {
                    found_xtra = true;
                    Boardstate new_left_board = new Boardstate(added_state.red, added_state.black, added_state.kings);

                    new_left_board.jump_red_piece(from_pos, local_to_pos, enemy_pos);

                    new_left_board = new_left_board.check_xtra_black_jmps(local_to_pos, new_left_board, ref move_count, possible_moves, ref found_xtra);

                    

                    possible_moves[move_count - 1] = new_left_board;

                    move_count++;
                }


                if (black_piece_can_move_right(to_pos, ref from_pos, ref local_to_pos, ref enemy_pos) == 2)
                {
                    found_xtra = true;
                    Boardstate new_rite_board = new Boardstate(added_state.red, added_state.black, added_state.kings);
                    new_rite_board.jump_red_piece(from_pos, local_to_pos, enemy_pos);

                    new_rite_board = new_rite_board.check_xtra_black_jmps(local_to_pos, new_rite_board, ref move_count, possible_moves, ref found_xtra);

                   

                    possible_moves[move_count - 1] = new_rite_board;

                    move_count++;
                }
            }

            return null;
        }
        void find_possible_black_moves(ref int moveCount)
        {
            moveCount = 0;

            int from_pos = -1;
            int to_pos = -1;
            int enemy_pos = -1;

            int mandatory_jump_check = check_mandatory_black_jumps();

            int i = 0;

            if (mandatory_jump_check != -1)
            {

                i = mandatory_jump_check;
            }

            for (; i < 32; i++)
            {
                if (pos_has_black_king(i))
                {
                    if (king_can_move_right(i, ref from_pos, ref to_pos, ref enemy_pos) != 0)
                    {

                        if (enemy_pos != -1)
                        {
                            bool found_xtra_states = false;
                            moveCount++;

                            Boardstate added_state = new Boardstate(this.red, this.black, this.kings);

                            added_state.jump_red_piece(from_pos, to_pos, enemy_pos);

                            added_state.check_xtra_black_king_jmps(to_pos, added_state, ref moveCount, possible_moves, ref found_xtra_states);


                            if (found_xtra_states == false)
                            {
                                
                                possible_moves[moveCount - 1] = added_state;
                            }
                            else
                            {
                                moveCount--;
                            }
                            enemy_pos = -1;
                        }
                        else if (enemy_pos == -1)
                        {
                            
                            int alternate_from = -1;
                            int alterante_to = -1;
                            int alternate_enemy = -1;

                            king_can_move_left(i, ref alternate_from, ref alterante_to, ref alternate_enemy);


                            if (alternate_enemy == -1)
                            {

                                king_can_move_back_right(i, ref alternate_from, ref alterante_to, ref alternate_enemy);
                            }

                            if (alternate_enemy == -1)
                            {

                                king_can_move_back_left(i, ref alternate_from, ref alterante_to, ref alternate_enemy);
                            }



                            if (alternate_enemy == -1)
                            {
                                moveCount++;
                                Boardstate added_state = new Boardstate(this.red, this.black, this.kings);
                                added_state.move_black_piece(from_pos, to_pos);

                                possible_moves[moveCount - 1] = added_state;

                            }

                        }



                    }
                    if (king_can_move_left(i, ref from_pos, ref to_pos, ref enemy_pos) != 0)
                    {
                        if (enemy_pos != -1)
                        {
                            bool found_xtra_states = false;
                            moveCount++;

                            Boardstate added_state = new Boardstate(this.red, this.black, this.kings);

                            added_state.jump_red_piece(from_pos, to_pos, enemy_pos);
                            added_state.check_xtra_black_king_jmps(to_pos, added_state, ref moveCount, possible_moves, ref found_xtra_states);


                            if (found_xtra_states == false)
                            {
                                
                                possible_moves[moveCount - 1] = added_state;
                            }
                            else
                            {
                                moveCount--;
                            }
                            enemy_pos = -1;
                        }
                        else if (enemy_pos == -1)
                        {

                            int alternate_from = -1;
                            int alterante_to = -1;
                            int alternate_enemy = -1;

                            if (alternate_enemy == -1)//we keep checking for consume necessity
                            {
                                king_can_move_right(i, ref alternate_from, ref alterante_to, ref alternate_enemy);
                            }

                            if (alternate_enemy == -1)
                            {

                                king_can_move_back_right(i, ref alternate_from, ref alterante_to, ref alternate_enemy);
                            }

                            if (alternate_enemy == -1)
                            {

                                king_can_move_back_left(i, ref alternate_from, ref alterante_to, ref alternate_enemy);
                            }

                            if (alternate_enemy == -1)
                            {
                                moveCount++;
                                Boardstate added_state = new Boardstate(this.red, this.black, this.kings);
                                added_state.move_black_piece(from_pos, to_pos);

                                possible_moves[moveCount - 1] = added_state;
                            }

                        }
                    }
                    if (king_can_move_back_right(i, ref from_pos, ref to_pos, ref enemy_pos) != 0)
                    {
                        if (enemy_pos != -1)
                        {
                            bool found_xtra_states = false;
                            moveCount++;

                            Boardstate added_state = new Boardstate(this.red, this.black, this.kings);
                            added_state.jump_red_piece(from_pos, to_pos, enemy_pos);
                            added_state.check_xtra_black_king_jmps(to_pos, added_state, ref moveCount, possible_moves, ref found_xtra_states);

                            if (found_xtra_states == false)
                            {
                                
                                possible_moves[moveCount - 1] = added_state;
                            }
                            else
                            {
                                moveCount--;
                            }

                            enemy_pos = -1;
                        }
                        else if (enemy_pos == -1)
                        {
                            int alternate_from = -1;
                            int alterante_to = -1;
                            int alternate_enemy = -1;

                            king_can_move_left(i, ref alternate_from, ref alterante_to, ref alternate_enemy);


                            if (alternate_enemy == -1)
                            {

                                king_can_move_back_left(i, ref alternate_from, ref alterante_to, ref alternate_enemy);
                            }

                            if (alternate_enemy == -1)//we keep checking for consume necessity
                            {
                                king_can_move_right(i, ref alternate_from, ref alterante_to, ref alternate_enemy);
                            }


                            if (alternate_enemy == -1)
                            {
                                moveCount++;
                                Boardstate added_state = new Boardstate(this.red, this.black, this.kings);
                                added_state.move_black_piece(from_pos, to_pos);

                                possible_moves[moveCount - 1] = added_state;

                            }

                        }
                    }
                    if (king_can_move_back_left(i, ref from_pos, ref to_pos, ref enemy_pos) != 0)
                    {
                        if (enemy_pos != -1)
                        {
                            bool found_xtra_states = false;

                            moveCount++;

                            Boardstate added_state = new Boardstate(this.red, this.black, this.kings);
                            added_state.jump_red_piece(from_pos, to_pos, enemy_pos);
                            added_state.check_xtra_black_king_jmps(to_pos, added_state, ref moveCount, possible_moves, ref found_xtra_states);

                            if (found_xtra_states == false)
                            {
                                
                                possible_moves[moveCount - 1] = added_state;
                            }
                            else
                            {
                                moveCount--;

                            }
                            enemy_pos = -1;
                        }
                        else if (enemy_pos == -1)
                        {
                            int alternate_from = -1;
                            int alterante_to = -1;
                            int alternate_enemy = -1;

                            king_can_move_left(i, ref alternate_from, ref alterante_to, ref alternate_enemy);
                            if (alternate_enemy == -1)//we keep checking for consume necessity
                            {
                                king_can_move_right(i, ref alternate_from, ref alterante_to, ref alternate_enemy);
                            }
                            if (alternate_enemy == -1)
                            {

                                king_can_move_back_right(i, ref alternate_from, ref alterante_to, ref alternate_enemy);
                            }





                            if (alternate_enemy == -1)
                            {
                                moveCount++;
                                Boardstate added_state = new Boardstate(this.red, this.black, this.kings);
                                added_state.move_black_piece(from_pos, to_pos);

                                possible_moves[moveCount - 1] = added_state;
                            }

                        }
                    }
                }
                else
                {
                    if (black_piece_can_move_left(i, ref from_pos, ref to_pos, ref enemy_pos) != 0)
                    {
                        if (enemy_pos != -1)
                        {

                            bool found_xtra_states = false;
                            moveCount++;

                            Boardstate added_state = new Boardstate(this.red, this.black, this.kings);


                            added_state.jump_red_piece(from_pos, to_pos, enemy_pos);

                            added_state.check_xtra_black_jmps(to_pos, added_state, ref moveCount, possible_moves, ref found_xtra_states);

                            if (found_xtra_states == false)//if this was not a final state, it cannot be added
                            {
                                possible_moves[moveCount - 1] = added_state;
                            }
                            else
                            {
                                moveCount--;
                            }

                            enemy_pos = -1;
                        }
                        else if(enemy_pos==-1)
                        {

                            int alternate_from = -1;
                            int alterante_to = -1;
                            int alternate_enemy = -1;

                            black_piece_can_move_right(i, ref alternate_from, ref alterante_to, ref alternate_enemy);



                            if (alternate_enemy == -1)
                            {//we cannot move if an enemy must be eaten

                                moveCount++;

                                Boardstate added_state = new Boardstate(this.red, this.black, this.kings);
                                added_state.move_black_piece(from_pos, to_pos);

                                possible_moves[moveCount - 1] = added_state;
                            }
                        }


                    }
                    if (black_piece_can_move_right(i, ref from_pos, ref to_pos, ref enemy_pos) != 0)
                    {
                        if (enemy_pos != -1)
                        {
                            moveCount++;
                            bool found_xtra_states = false;

                            Boardstate added_state = new Boardstate(this.red, this.black, this.kings);
                            added_state.jump_red_piece(from_pos, to_pos, enemy_pos);

                            added_state.check_xtra_black_jmps(to_pos, added_state, ref moveCount, possible_moves, ref found_xtra_states);

                            if (found_xtra_states == false)//if this was not a final state, it cannot be added
                            {

                                possible_moves[moveCount - 1] = added_state;
                            }
                            else
                            {
                                moveCount--;

                            }
                            enemy_pos = -1;
                        }
                        else if(enemy_pos==-1)
                        {
                            int alternate_from = -1;
                            int alterante_to = -1;
                            int alternate_enemy = -1;

                            black_piece_can_move_left(i, ref alternate_from, ref alterante_to, ref alternate_enemy);



                            if (alternate_enemy == -1)
                            {//we cannot move if an enemy must be eaten

                                moveCount++;

                                Boardstate added_state = new Boardstate(this.red, this.black, this.kings);

                                added_state.move_black_piece(from_pos, to_pos);

                                possible_moves[moveCount - 1] = added_state;
                            }
                        }
                    }
                }
            }


            //MessageBox.Show("total possible move count "+moveCount);


        }

        void jump_red_piece(int from_pos, int to_pos, int enemy_pos)//this eliminates given black piece
        {
            if (pos_has_black_king(from_pos))
            {
                this.black = this.black ^= (uint)(1 << from_pos);

                this.red = this.red ^= (uint)(1 << enemy_pos);//delete enemy

                this.kings = this.kings ^= (uint)(1 << from_pos);

                if (pos_has_red_king(enemy_pos))//if there is a black king eliminate him
                {
                    this.kings = this.kings ^= (uint)(1 << enemy_pos);
                }

                this.black = this.black | (uint)(1 << to_pos);
                this.kings = this.kings | (uint)(1 << to_pos);
            }
            else
            {
                if (pos_has_black_piece(from_pos))
                {
                    this.black = this.black ^= (uint)(1 << from_pos);
                    this.red = this.red ^= (uint)(1 << enemy_pos);//delete enemy
                    this.black = this.black | (uint)(1 << to_pos);

                    if (to_pos == 0 || to_pos == 1 || to_pos == 2 || to_pos == 3)
                    {
                        become_black_king(to_pos);
                    }
                }
                else
                {
                    MessageBox.Show("Wrong piece color selected");
                }
            }
        }

        public int calculate_heuristic()
        {
            int value = 1;//default value returned

            for(int i= 0; i < 32; i++)
            {
                if (pos_has_red_piece(i))
                {
                    if (pos_has_red_king(i))//an already aquired king is worth more
                    {
                        value = value + 100;
                    }
                    else
                    {
                        value = value + 10;
                    }
                    if(i<3)//backrow pieces are worth more
                    {
                        value = value + 10;
                    }
                    if (i > 15)//pieces in opponents half of the board are worth more
                    {
                        value = value + 10;
                    }

                    

                }
                else if (pos_has_black_piece(i))
                {
                    if (pos_has_black_king(i))//an already aquired king is worth more
                    {
                        value = value - 100;
                    }
                    else
                    {
                        value = value - 10;
                    }
                    if (i > 27)//backrow pieces are worth more
                    {
                        value = value - 10;
                    }
                    if (i < 16 )//pieces in opponents half of the board are worth more
                    {
                        value = value -10;
                    }

                }

            }


            return value;


        }
        void jump_black_piece(int from_pos, int to_pos, int enemy_pos)//this eliminates given black piece
        {
            if (pos_has_red_king(from_pos))
            {
                this.red = this.red ^= (uint)(1 << from_pos);

                this.black = this.black ^= (uint)(1 << enemy_pos);//delete enemy

                this.kings = this.kings ^= (uint)(1 << from_pos);

                if (pos_has_black_king(enemy_pos))//if there is a black king eliminate him
                {
                    this.kings = this.kings ^= (uint)(1 << enemy_pos);
                }

                this.red = this.red | (uint)(1 << to_pos);
                this.kings = this.kings | (uint)(1 << to_pos);
            }
            else
            {
                if (pos_has_red_piece(from_pos))
                {
                    this.red = this.red ^= (uint)(1 << from_pos);
                    this.black = this.black ^= (uint)(1 << enemy_pos);//delete enemy
                    this.red = this.red | (uint)(1 << to_pos);

                    if (to_pos == 28 || to_pos == 29 || to_pos == 30 || to_pos == 31)
                    {
                        become_red_king(to_pos);
                    }
                }
                else
                {
                    MessageBox.Show("Wrong piece color selected");
                }
            }
        }
        
        int check_mandatory_red_jumps()
        {
            int must_jump = -1;

            for (int i= 0; i < 32; i++){
                
                if (pos_has_red_king(i))
                {


                    if (king_must_jump_left(i) || king_must_jump_right(i) || king_must_jump__back_left(i) || king_must_jump__back_right(i))
                    {
                        must_jump = i;
                    }
                   
                }
                else if (pos_has_red_piece(i))
                {
                    if (red_piece_must_jump_left(i) || red_piece_must_jump_right(i))
                    {
                        must_jump = i;
                    }
                }
            }

            return must_jump;

        }

        int check_mandatory_black_jumps()
        {
            int must_jump = -1;

            for (int i = 0; i < 32; i++)
            {

                if (pos_has_black_king(i))
                {

                    if (king_must_jump_left(i) || king_must_jump_right(i) || king_must_jump__back_left(i) || king_must_jump__back_right(i))
                    {
                        must_jump = i;
                    }
                  
                }
                else if (pos_has_black_piece(i))
                {
                    if (black_piece_must_jump_left(i) || black_piece_must_jump_right(i))
                    {
                        must_jump = i;
                    }
                }
            }

            return must_jump;

        }
        void find_possible_red_moves(ref int moveCount)
        {
            moveCount = 0;

            int from_pos = -1;
            int to_pos = -1;
            int enemy_pos = -1;
            int mandatory_jump_check = check_mandatory_red_jumps();

            int i = 0;

            if (mandatory_jump_check != -1)
            {

                i = mandatory_jump_check;
            }
            for (; i<32; i++)
            {
               
                if (pos_has_red_king(i))
                {
                    if(king_can_move_right(i,ref from_pos,ref to_pos, ref enemy_pos) != 0)
                    {

                        if (enemy_pos != -1)
                        {
                           
                            moveCount++;
                            bool found_xtra_states = false;
                            Boardstate added_state = new Boardstate(this.red, this.black, this.kings);
                            added_state.jump_black_piece(from_pos, to_pos, enemy_pos);

                            added_state.check_xtra_red_king_jmps(to_pos, added_state, ref moveCount, possible_moves, ref found_xtra_states);


                            if (found_xtra_states == false)
                            {

                                possible_moves[moveCount - 1] = added_state;
                            }
                            else
                            {
                                moveCount--;
                            }                                           
                          


                            enemy_pos = -1;
                        }
                        else if (enemy_pos == -1)
                        {

                            int alternate_from = -1;
                            int alterante_to = -1;
                            int alternate_enemy = -1;

                            king_can_move_left(i, ref alternate_from, ref alterante_to, ref alternate_enemy);


                            if (alternate_enemy == -1)
                            {

                                king_can_move_back_right(i, ref alternate_from, ref alterante_to, ref alternate_enemy);
                            }

                            if (alternate_enemy == -1)
                            {

                                king_can_move_back_left(i, ref alternate_from, ref alterante_to, ref alternate_enemy);
                            }



                            if (alternate_enemy == -1)
                            {
                                moveCount++;
                                Boardstate added_state = new Boardstate(this.red, this.black, this.kings);
                                added_state.move_red_piece(from_pos, to_pos);

                                possible_moves[moveCount - 1] = added_state;

                            }
                        }

                         

                    }
                    if(king_can_move_left(i, ref from_pos, ref to_pos, ref enemy_pos) != 0)
                    {

                        if (enemy_pos != -1)
                        {
                            bool found_xtra_states = false;
                            moveCount++;

                            Boardstate added_state = new Boardstate(this.red, this.black, this.kings);
                            added_state.jump_black_piece(from_pos, to_pos, enemy_pos);
                            added_state.check_xtra_red_king_jmps(to_pos, added_state, ref moveCount, possible_moves, ref found_xtra_states);

                            if (found_xtra_states == false)
                            {

                                possible_moves[moveCount - 1] = added_state;
                            }
                            else
                            {
                                moveCount--;
                            }


                            enemy_pos = -1;
                        }
                        else if (enemy_pos == -1)
                        {


                            int alternate_from = -1;
                            int alterante_to = -1;
                            int alternate_enemy = -1;

                            if (alternate_enemy == -1)//we keep checking for consume necessity
                            {
                                king_can_move_right(i, ref alternate_from, ref alterante_to, ref alternate_enemy);
                            }

                            if (alternate_enemy == -1)
                            {

                                king_can_move_back_right(i, ref alternate_from, ref alterante_to, ref alternate_enemy);
                            }

                            if (alternate_enemy == -1)
                            {

                                king_can_move_back_left(i, ref alternate_from, ref alterante_to, ref alternate_enemy);
                            }

                            if (alternate_enemy == -1)
                            {
                                moveCount++;
                                Boardstate added_state = new Boardstate(this.red, this.black, this.kings);
                                added_state.move_red_piece(from_pos, to_pos);

                                possible_moves[moveCount - 1] = added_state;
                            }
                        }
                    }
                    if (king_can_move_back_right(i, ref from_pos, ref to_pos, ref enemy_pos) != 0)
                    {
                        if (enemy_pos != -1)
                        {
                            bool found_xtra_states = false;
                            moveCount++;

                            Boardstate added_state = new Boardstate(this.red, this.black, this.kings);
                            added_state.jump_black_piece(from_pos, to_pos, enemy_pos);
                            added_state.check_xtra_red_king_jmps(to_pos, added_state, ref moveCount, possible_moves, ref found_xtra_states);

                            if (found_xtra_states == false)
                            {

                                possible_moves[moveCount - 1] = added_state;
                            }
                            else
                            {
                                moveCount--;
                            }



                            enemy_pos = -1;
                        }
                        else if (enemy_pos == -1)
                        {

                            int alternate_from = -1;
                            int alterante_to = -1;
                            int alternate_enemy = -1;

                            king_can_move_left(i, ref alternate_from, ref alterante_to, ref alternate_enemy);

                          
                            if (alternate_enemy == -1)
                            {

                                king_can_move_back_left(i, ref alternate_from, ref alterante_to, ref alternate_enemy);
                            }

                            if (alternate_enemy == -1)//we keep checking for consume necessity
                            {
                                king_can_move_right(i, ref alternate_from, ref alterante_to, ref alternate_enemy);
                            }


                            if (alternate_enemy == -1)
                            {
                                moveCount++;
                                Boardstate added_state = new Boardstate(this.red, this.black, this.kings);
                                added_state.move_red_piece(from_pos, to_pos);

                                possible_moves[moveCount - 1] = added_state;
                            }
                        }
                    }
                    if (king_can_move_back_left(i, ref from_pos, ref to_pos, ref enemy_pos) != 0)
                    {
                        if (enemy_pos != -1)
                        {
                            bool found_xtra_states = false;
                            moveCount++;

                            Boardstate added_state = new Boardstate(this.red, this.black, this.kings);
                            added_state.jump_black_piece(from_pos, to_pos, enemy_pos);
                            added_state.check_xtra_red_king_jmps(to_pos, added_state, ref moveCount, possible_moves, ref found_xtra_states);

                            if (found_xtra_states == false)
                            {

                                possible_moves[moveCount - 1] = added_state;
                            }
                            else
                            {
                                moveCount--;
                            }


                            enemy_pos = -1;
                        }
                        else if (enemy_pos == -1)
                        {

                            int alternate_from = -1;
                            int alterante_to = -1;
                            int alternate_enemy = -1;

                            king_can_move_left(i, ref alternate_from, ref alterante_to, ref alternate_enemy);
                            if (alternate_enemy == -1)//we keep checking for consume necessity
                            {
                                king_can_move_right(i, ref alternate_from, ref alterante_to, ref alternate_enemy);
                            }
                            if (alternate_enemy == -1)
                            {

                                king_can_move_back_right(i, ref alternate_from, ref alterante_to, ref alternate_enemy);
                            }

                          



                            if (alternate_enemy == -1)
                            {
                                moveCount++;
                                Boardstate added_state = new Boardstate(this.red, this.black, this.kings);
                                added_state.move_red_piece(from_pos, to_pos);

                                possible_moves[moveCount - 1] = added_state;

                            }
                        }
                    }
                }
                else
                {
                    if (red_piece_can_move_left(i, ref from_pos, ref to_pos, ref enemy_pos) != 0)
                    {

                        if (enemy_pos != -1)
                        {
                            bool found_xtra_states = false;

                            moveCount++;

                            Boardstate added_state = new Boardstate(this.red, this.black, this.kings);
                            added_state.jump_black_piece(from_pos, to_pos, enemy_pos);

                            added_state.check_xtra_red_jmps(to_pos, added_state, ref moveCount, possible_moves, ref found_xtra_states);

                            if (found_xtra_states == false) { 
                                possible_moves[moveCount - 1] = added_state;
                            }
                            else
                            {
                                moveCount--;
                            }
                            enemy_pos = -1;
                        }
                        else if (enemy_pos == -1)
                        {

                            int alternate_from = -1;
                            int alterante_to = -1;
                            int alternate_enemy = -1;

                            red_piece_can_move_right(i, ref alternate_from, ref alterante_to, ref alternate_enemy);



                            if (alternate_enemy == -1) {//we cannot move if an enemy must be eaten

                                moveCount++;
                                Boardstate added_state = new Boardstate(this.red, this.black, this.kings);
                                added_state.move_red_piece(from_pos, to_pos);

                                possible_moves[moveCount - 1] = added_state;
                            }


                            
                        }


                    }
                    if (red_piece_can_move_right(i, ref from_pos, ref to_pos, ref enemy_pos) != 0)
                    {

                        if (enemy_pos != -1)
                        {
                            moveCount++;
                            bool found_xtra_states = false;
                            Boardstate added_state = new Boardstate(this.red, this.black, this.kings);


                            added_state.jump_black_piece(from_pos, to_pos, enemy_pos);
                            added_state.check_xtra_red_jmps(to_pos, added_state, ref moveCount, possible_moves, ref found_xtra_states);

                            if (found_xtra_states == false)
                            {

                                possible_moves[moveCount - 1] = added_state;
                            }
                            else
                            {
                                moveCount--;
                            }
                            enemy_pos = -1;
                        }
                        else if (enemy_pos == -1)
                        {

                            int alternate_from = -1;
                            int alterante_to = -1;
                            int alternate_enemy = -1;

                            red_piece_can_move_left(i, ref alternate_from, ref alterante_to, ref alternate_enemy);



                            if (alternate_enemy == -1)
                            {//we cannot move if an enemy must be eaten

                                moveCount++;

                                Boardstate added_state = new Boardstate(this.red, this.black, this.kings);

                                added_state.move_red_piece(from_pos, to_pos);

                                possible_moves[moveCount - 1] = added_state;
                            }
                        }
                    }
                }

            }

           
            //MessageBox.Show("total possible move count "+moveCount);

            
        }

        public bool is_win(int player)
        {
            bool game_is_over = false;

            if (win_pre_calc)
            {
                if (player == 2 && win_board == int.MaxValue)
                {
                    game_is_over = true;
                }
                else if (player == 1 && win_board == int.MinValue)
                {
                    game_is_over = true;
                }
            }
            else
            {
                int move_ref = -1;

                if(player == 2)
                {
                    Boardstate[] test_board_states = new Boardstate[48];
                    test_board_states=GetMoves(ref move_ref, CheckerType.Black, this.black,this.red,this.kings);

                    if(move_ref==0 && test_board_states[0] == null)
                    {
                        game_is_over = true;
                    }

                }
                else
                {

                    Boardstate[] test_board_states = new Boardstate[48];

                    test_board_states=GetMoves(ref move_ref, CheckerType.Red, this.black, this.red, this.kings);

                    if (move_ref == 0 && test_board_states[0] == null)
                    {
                        game_is_over = true;
                    }
                }
            }
           

            return game_is_over;
        }
        public Boardstate[] GetMoves(ref int moveCount, CheckerType color, uint black, uint red, uint kings)
        {

            this.black = black;
            this.red = red;
            this.kings = kings;

            if (color == CheckerType.Red)
            {

                find_possible_red_moves(ref moveCount);

                if (moveCount == 0 && possible_moves[0]==null)
                {
                    win_board = int.MinValue;
                    win_pre_calc = true;
                }


            }
            else
            {
                find_possible_black_moves(ref moveCount);
                if (moveCount == 0 && possible_moves[0] == null)
                {
                    win_board = int.MaxValue;
                    win_pre_calc = true;
                }

            }


            return possible_moves;
        }

    }
}
