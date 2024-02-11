var GymMembershipPlan = {
    Initialize: function () {
        GymMembershipPlan.constructor();
    },
    constructor: function () {
    },

    ChangePlanType: function () {
        var planType = $("#PlanTypeEnumId option:selected").text();
        if (planType == "Duration") {
            $("#PlanDurationInMonthDivId").show();
            $("#PlanDurationInDaysDivId").show();
            $("#PlanDurationInSessionDivId").hide();
        } else {
            $("#PlanDurationInMonthDivId").hide();
            $("#PlanDurationInDaysDivId").hide();
            $("#PlanDurationInSessionDivId").show();
        }
    }
}
