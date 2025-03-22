namespace CBRE.FacilityManagement.Audit.Core.Harbour.Models.Enums
{
    public enum AuditStatus
    {
        /// <summary>
        /// Open
        /// </summary>
        Open,
        
        /// <summary>
        /// When conducting has been started and is in progress.
        /// </summary>
        InProgress,

        /// <summary>
        /// When conducting has been put on hold.
        /// </summary>
        OnHold,

        /// <summary>
        /// When conducting has been completed.
        /// </summary>
        Completed,

        /// <summary>
        /// When conducting started but not completed.
        /// </summary>
        Started,

        /// <summary>
        /// When conducting has been cancelled.
        /// </summary>
        Cancelled,

        /// <summary>
        /// When audit has been closed.
        /// </summary>
        Close
    }
}
