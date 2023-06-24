﻿namespace Coditech.Common.API.Model.Response
{
    public abstract class BaseListResponse : BaseResponse
	{
		public int? PageIndex { get; set; }
		public int? PageSize { get; set; }
		public int? TotalPages { get; set; }
		public int? TotalResults { get; set; }
	}
}
