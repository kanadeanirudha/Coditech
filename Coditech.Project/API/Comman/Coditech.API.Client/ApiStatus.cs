﻿using System.Net;

namespace Coditech.API.Client
{
    public class ApiStatus
	{
		public int? ErrorCode { get; set; }
		public string ErrorMessage { get; set; }
		public bool HasError { get; set; }
		public HttpStatusCode StatusCode { get; set; }
		public int RequestTimeOut { get; set; } = 100000;
	}
}
