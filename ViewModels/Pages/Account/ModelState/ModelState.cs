using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Pages.Account.ModelState;

public class ModelState<TErrorModel,TResultModel>
    where TErrorModel : class where TResultModel : class
{
    public string? type { get; set; }
    public string? title { get; set; }
    public int? status { get;set; }
    public string? traceId { get; set; }
    public string? instance { get; set; }
    public TErrorModel? errors { get; set; }
    public TResultModel? result { get; set; }
}
public class ModelState
{
	public string? type { get; set; }
	public string? title { get; set; }
	public int? status { get; set; }
	public string? traceId { get; set; }
	public string? instance { get; set; }
}