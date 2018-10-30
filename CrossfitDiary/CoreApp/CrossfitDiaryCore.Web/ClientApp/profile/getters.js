// profile/getters.ts
export var getters = {
    fullName: function (state) {
        debugger;
        var user = state.user;
        var firstName = (user && user.firstName) || "";
        var lastName = (user && user.lastName) || "";
        return firstName + " " + lastName;
    }
};
//# sourceMappingURL=getters.js.map