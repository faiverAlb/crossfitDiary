var WindowHelper = /** @class */ (function () {
    function WindowHelper() {
    }
    WindowHelper.scrollToTargetAdjusted = function (element) {
        // var element = document.getElementById("targetElement");
        var offset = 30;
        var bodyRect = document.body.getBoundingClientRect().top;
        var elementRect = element.getBoundingClientRect().top;
        var elementPosition = elementRect - bodyRect;
        var offsetPosition = elementPosition - offset;
        window.scrollTo({
            top: offsetPosition,
            behavior: "smooth"
        });
    };
    return WindowHelper;
}());
export { WindowHelper };
//# sourceMappingURL=WindowHelper.js.map