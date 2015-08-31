using ClearMeasure.Bootcamp.Core.Features.Workflow;
using ClearMeasure.Bootcamp.Core.Services;

namespace ClearMeasure.Bootcamp.Core.Model.ExpenseReportWorkflow
{
    public class DraftToSubmittedCommand : StateCommandBase
    {
        public DraftToSubmittedCommand()
            : base()
        {
        }

        public override string TransitionVerbPresentTense
        {
            get { return "Submit"; }
        }

        public override string TransitionVerbPastTense
        {
            get { return "Submitted"; }
        }

        public override ExpenseReportStatus GetBeginStatus()
        {
            return ExpenseReportStatus.Draft;
        }

        protected override ExpenseReportStatus GetEndStatus()
        {
            return ExpenseReportStatus.Submitted;
        }

        protected override bool userCanExecute(Employee currentUser, ExpenseReport report)
        {
            return currentUser == report.Submitter;
        }

        protected override void preExecute(ExecuteTransitionCommand transitionCommand)
        {
            transitionCommand.Report2.LastSubmitted = transitionCommand.CurrentDate2;
            if (transitionCommand.Report2.FirstSubmitted == null)
            {
                transitionCommand.Report2.FirstSubmitted = transitionCommand.CurrentDate2;
            }
        }
    }
}