using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace WorldSeriesChampions
{
    class VM : INotifyPropertyChanged
    {
        #region constants
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
        #endregion
        #region properties
        public ObservableCollection<Team> Teams { get; set; } = new ObservableCollection<Team>();

        private string teamName;
        public string TeamName
        {
            get { return teamName; }
            set { teamName = value; propChanged(); }
        }

        private int timesWon;
        public int TimesWon
        {
            get { return timesWon; }
            set { timesWon = value; propChanged(); }
        }

        private string yearsWon;
        public string YearsWon
        {
            get { return yearsWon; }
            set { yearsWon = value; propChanged(); }
        }
        #endregion
        #region methods
        public void init()
        {
            string[] TeamsList = File.ReadAllLines(TeamsFilePath);
            string[] SeriesWinners = File.ReadAllLines(SeriesWinnersFilePath);

            foreach (string teamName in TeamsList)
            {
                Team t = new Team();
                t.Name = teamName;
                //t.Id = Teams.Count;
                int i = 0;
                for (int year = SERIES_FIRST_YEAR; year <= SERIES_LAST_YEAR; year++)
                {
                    if (!SERIES_NOT_PLAYED_YEARS.Contains(year))
                    {
                        if (SeriesWinners[i] == teamName)
                            t.WinningYears.Add(year);
                        i++;
                    }
                }
                Teams.Add(t);
            }
        }

        public void TeamSelected(int index)
        {
            TeamName = Teams[index].Name;
            TimesWon = Teams[index].WinningYears.Count;
            YearsWon = string.Join(", ", Teams[index].WinningYears);
        }
        #endregion
        #region prop changed
        public event PropertyChangedEventHandler PropertyChanged;
        private void propChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}
