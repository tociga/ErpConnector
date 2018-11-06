using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpConnector.Ax.DTO
{
    public class ProdTableDTO
    {
        public long RecId { get; set; }
        public string DataAreaId { get; set; }
        public long Partition { get; set; }
        public int RecVersion { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime BOMDate { get; set; }
        public string BOMId { get; set; }
        public DateTime CalcDate { get; set; }
        public decimal Denisity { get; set; }
        public decimal Depth { get; set; }
        public DateTime DlvDate { get; set; }
        public int DlvTime { get; set; }
        public DateTime FinishedDate { get; set; }
        public decimal Height { get; set; }
        public string InventDimId { get; set; }
        public string InventTransId { get; set; }
        public string ItemId { get; set; }
        public DateTime LatestSchedDate { get; set; }
        public decimal LatestSchedTime { get; set; }
        public int ProdStatus { get; set; }
        public int ProdType { get; set; }
        public decimal QtyCalc { get; set; }
        public decimal QtySched { get; set; }
        public decimal QtyStUp { get; set; }
        public DateTime RealDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Reservation { get; set; }
        public DateTime SchedDate { get; set; }
        public DateTime SchedEnd { get; set; }
        public decimal SchedFromTime { get; set; }
        public DateTime SchedStart { get; set; }
        public int SchedStatus { get; set; }
        public decimal SchedToTime { get; set; }
        public decimal Width { get; set; }
        public string ProdId { get; set; }
        public decimal RemainInventPhysical { get; set; }


    }
}
