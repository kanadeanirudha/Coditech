﻿namespace Coditech.Common.Helper.Utilities
{
    public enum DropdownTypeEnum
    {
        City,
        AccessibleCentre,
        CentrewiseDepartment,
        CentrewiseBuilding,
        Department,
        Designation,
        Organisation,
        RegionalOffice,
        Centre,
        TaxGroup,
        Country,
        Region,
        ModuleList,
        MenuList,
        Nationality,
        Gender,
        IndentificationType,
        MaritalStatus,
        BloodGroups,
        Title,
        Occupation,
        GymGroup,
        GymSource,
        GymFollowupTypes,
        MeasurementUnit,
        Employee,
        GymPlanType,
        FinancialYear,
        GeneralRunningNumberFor
    }

    public enum GeneralSystemGlobleSettingEnum
    {
        IsGymMemberLogin,
        IsEmployeeLogin,
        ActiveProjectName,
        DefaultPassword
    }

    public enum UserTypeEnum
    {
        Admin,
        Employee,
        Customer,
        GymMember

    }

    public enum ActiveProjectNameEnum
    {
        GMS,
        HMS
    }

    public enum MediaTypeEnum
    {
        Image,
        File,
        Video,
        Audio
    }

    public enum AddressTypeEnum
    {
        PermanentAddress,
        CorrespondanceAddress,
        BusinessAddress        
    }
}
