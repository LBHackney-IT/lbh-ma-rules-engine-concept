namespace ManagedArrearsRuleEngine.Tests.Domain.Entities
{
    public enum ArrearsAction 
    {
        None=0,
        ArrearsCleared,
        EarlySmsWarning,
        Letter1Sent,
        Letter2Sent,
        ItInvestigationCompleted,
        PanelOutcome,
        CourtWarningLetterSent,
        ApplyForCountDate
    }
}
