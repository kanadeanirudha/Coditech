namespace Coditech.Common.API.Model
{
    public class DBTMTrainerDashboardModel : BaseModel
    {
        public int NumberOfTrainees { get; set; }
        public long TotalNumberOfActivityPerformedDuringWeek { get; set; }
        public string TopActivityPerformed { get; set; }
        public List<AssignmentModel> DueTodayAssignments { get; set; }
        public List<TraineeModel> Top3Trainee { get; set; }
        
    }
}
