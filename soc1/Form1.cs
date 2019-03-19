using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace soc1
{
    public partial class Form1 : Form
    {
        SQLiteConnection m_dbConnection;
        int[,,] systempolski = new int[17, 9, 2]{
            {{18,1},{2,17},{3,16},{4,15},{5,14},{6,13},{7,12},{8,11},{9,10}},
            {{10,18},{11,9},{12,8},{13,7},{14,6},{15,5},{16,4},{17,3},{1,2}},
            {{18,2},{3,1},{4,17},{5,16},{6,15},{7,14},{8,13},{9,12},{10,11}},
            {{11,18},{12,10},{13,9},{14,8},{15,7},{16,6},{17,5},{1,4},{2,3}},
            {{18,3},{4,2},{5,1},{6,17},{7,16},{8,15},{9,14},{10,13},{11,12}},
            {{12,18},{13,11},{14,10},{15,9},{16,8},{17,7},{1,6},{2,5},{3,4}},
            {{18,4},{5,3},{6,2},{7,1},{8,17},{9,16},{10,15},{11,14},{12,13}},
            {{13,18},{14,12},{15,11},{16,10},{17,9},{1,8},{2,7},{3,6},{4,5}},
            {{5,18},{6,4},{7,3},{8,2},{9,1},{10,17},{11,16},{12,15},{13,14}},
            {{18,14},{15,13},{16,12},{17,11},{1,10},{2,9},{3,8},{4,7},{5,6}},
            {{6,18},{7,5},{8,4},{9,3},{10,2},{11,1},{12,17},{13,16},{14,15}},
            {{18,15},{16,14},{17,13},{1,12},{2,11},{3,10},{4,9},{5,8},{6,7}},
            {{7,18},{8,6},{9,5},{10,4},{11,3},{12,2},{13,1},{14,17},{15,16}},
            {{18,16},{17,15},{1,14},{2,13},{3,12},{4,11},{5,10},{6,9},{7,8}},
            {{8,18},{9,7},{10,6},{11,5},{12,4},{13,3},{14,2},{15,1},{16,17}},
            {{18,17},{1,16},{2,15},{3,14},{4,13},{5,12},{6,11},{7,10},{8,9}},
            {{9,18},{10,8},{11,7},{12,6},{13,5},{14,4},{15,3},{16,2},{17,1}}
            };
        

        private void initdb2()
        {
            m_dbConnection = new SQLiteConnection("Data Source=liga.sqlite;Version=3;");
            m_dbConnection.Open();
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // initdb1();
            initdb2();
            popliga();

        }

        private void popliga()
        {
            string sql = "select * from ligi";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                comboBox1.Items.Add(reader[0]);

//                Console.WriteLine("Name: " + reader["name"] + "\tScore: " + reader["score"]);
            //Console.ReadLine();
            //comboBox1.Items.Add("a");
        }

        private void poptabela()
        {
            listView1.Items.Clear();
            ListViewItem litem;
            string sql = "select * from zespoly WHERE Z_LIGA='"+(comboBox1.SelectedIndex+1).ToString() +"'";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            int i = 1;
            while (reader.Read())
            {
                litem = new ListViewItem(new[] { i.ToString(),reader[1].ToString() });
                listView1.Items.Add(litem);
                i++;
                //listView1.Items.Add(reader[1].ToString());
               // listView1.Items[1][2] = "f";
            }
                //comboBox1.Items.Add(reader[0]);


        }

        private void initdb()
        {
            SQLiteConnection m_dbConnection;
            SQLiteConnection.CreateFile("MyDatabase.sqlite");

            m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            m_dbConnection.Open();


            string sql = "create table highscores (name varchar(20), score int)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "insert into highscores (name, score) values ('Me', 3000)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into highscores (name, score) values ('Myself', 6000)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into highscores (name, score) values ('And I', 9001)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = "select * from highscores order by score desc";
            command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                Console.WriteLine("Name: " + reader["name"] + "\tScore: " + reader["score"]);
            Console.ReadLine();


        }

        private void initdb1()
        {
            SQLiteConnection m_dbConnection;
            //SQLiteConnection.CreateFile("liga.sqlite");
            //SQLite
            string sql;
            SQLiteCommand command;

            m_dbConnection = new SQLiteConnection("Data Source=liga.sqlite;Version=3;");
            m_dbConnection.Open();


            /*       string sql = "create table highscores (name varchar(20), score int)";
                   SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                   command.ExecuteNonQuery();
           */
            StreamReader sr = new StreamReader("ligi.csv");
            string ln;
            string[] ll;
            for (int i=0;i<462;i++)
            {
                ln = sr.ReadLine();
                ll = ln.Split(',');
                sql = "insert into zespoly (Z_NAZWA, Z_LIGA) values ('" + ll[1] + "'," + ll[0] + ")";
                Console.WriteLine(sql);
                command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
            }
/*
            for (int i = 0; i < 27; i++)
            {
                ln = sr.ReadLine();
                ll = ln.Split(',');
                sql = "insert into ligi (L_NAZWA) values ('" + ll[1] + "')";
                command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
                Console.WriteLine(sql);
            }

            */

            /*
            sql = "select * from highscores order by score desc";
            command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                Console.WriteLine("Name: " + reader["name"] + "\tScore: " + reader["score"]);
            Console.ReadLine();
            */


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            poptabela();

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}



// http://www.altcontroldelete.pl/artykuly/c-wpf-oraz-sqlite-razem-w-jednym-projekcie/

/**
 *  https://blog.tigrangasparian.com/2012/02/09/getting-started-with-sqlite-in-c-part-one/
 * 
 namespace SQLiteSamples
{
    class Program
    {
        // Holds our connection with the database
        SQLiteConnection m_dbConnection;

        static void Main(string[] args)
        {
            Program p = new Program();
        }

        public Program()
        {
            createNewDatabase();
            connectToDatabase();
            createTable();
            fillTable();
            printHighscores();
        }

        // Creates an empty database file
        void createNewDatabase()
        {
            SQLiteConnection.CreateFile("MyDatabase.sqlite");
        }

        // Creates a connection with our database file.
        void connectToDatabase()
        {
            m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            m_dbConnection.Open();
        }

        // Creates a table named 'highscores' with two columns: name (a string of max 20 characters) and score (an int)
        void createTable()
        {
            string sql = "create table highscores (name varchar(20), score int)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        // Inserts some values in the highscores table.
        // As you can see, there is quite some duplicate code here, we'll solve this in part two.
        void fillTable()
        {
            string sql = "insert into highscores (name, score) values ('Me', 3000)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into highscores (name, score) values ('Myself', 6000)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into highscores (name, score) values ('And I', 9001)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        // Writes the highscores to the console sorted on score in descending order.
        void printHighscores()
        {
            string sql = "select * from highscores order by score desc";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                Console.WriteLine("Name: " + reader["name"] + "\tScore: " + reader["score"]);
            Console.ReadLine();
        }
    }
}
*/
