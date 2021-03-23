using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WorldSeriesChampions
{
    class VM : INotifyPropertyChanged
    {
        string TeamsFilePath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), RESULT_FOLDER_NAME, TEAMS_FILE_NAME);
        string SeriesWinnersFilePath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), RESULT_FOLDER_NAME, SERIES_WINNERS_FILE_NAME);

        const string RESULT_FOLDER_NAME = "WorldSeriesChampion-TeamsInfo";
        const string TEAMS_FILE_NAME = "Teams.txt";
        const string SERIES_WINNERS_FILE_NAME = "WorldSeriesWinners.txt";
        const int SERIES_FIRST_YEAR = 1903;
        const int SERIES_LAST_YEAR = 2012;
        static readonly int[] SERIES_NOT_PLAYED_YEARS = { 1904, 1994 };

        public ObservableCollection<Team> Teams { get; set; } = new ObservableCollection<Team>();

        public void init()
        {
            string[] TeamsList = File.ReadAllLines(TeamsFilePath);
            string[] SeriesWinners = File.ReadAllLines(SeriesWinnersFilePath);
            
            foreach(string teamName in TeamsList)
            {
                Team t = new Team();
                t.Name = teamName;

                int totalWinners = SeriesWinners.Length;
                int i = 0;
                for(int year = SERIES_FIRST_YEAR; year <= SERIES_LAST_YEAR; year++)
                {
                    if (!SERIES_NOT_PLAYED_YEARS.Contains(year)){
                        if (SeriesWinners[i] == teamName)
                            t.WinningYears.Add(year);
                    } else
                    {
                        i++;
                    }
                    i++;
                }
                if (t.WinningYears.Count > 0)
                    return;
                Teams.Add(t);
            }
        }

        #region prop changed
        public event PropertyChangedEventHandler PropertyChanged;
        private void propChange([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}
