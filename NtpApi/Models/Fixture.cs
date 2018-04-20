using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NtpApi.Models
{
    public class Fixture {

        [BsonId]
        public ObjectId Id { get; }
        public int Id_country { get; set; }
        public int Id_league { get; set; }
        public int Id_season { get; set; }
        public int Id_stage { get; set; }
        public int Id_team_season_away { get; set; }
        public int Id_team_season_home { get; set; }
        public int Team_season_away_name { get; set; }
        public int Team_season_home_name { get; set; }
        public string Round { get; set; }
        public string Schedule_date { get; set; }
        public string Fixture_status { get; set; }
        public string Fixture_status_short { get; set; }
        public string Stadium { get; set; }

    }
}
