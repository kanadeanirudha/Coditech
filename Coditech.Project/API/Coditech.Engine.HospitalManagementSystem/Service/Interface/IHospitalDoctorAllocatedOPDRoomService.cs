﻿using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IHospitalDoctorAllocatedOPDRoomService
    {
        HospitalDoctorAllocatedOPDRoomListModel GetHospitalDoctorAllocatedOPDRoomList(string selectedCentreCode, short selectedDepartmentId, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        HospitalDoctorAllocatedOPDRoomModel GetHospitalDoctorAllocatedOPDRoom(int hospitalDoctorId,int hospitalDoctorAllocatedOPDRoomId);
        bool UpdateHospitalDoctorAllocatedOPDRoom(HospitalDoctorAllocatedOPDRoomModel model);
    }
}
