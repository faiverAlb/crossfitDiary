var SpinnerModel = /** @class */ (function () {
    function SpinnerModel(status, size, color, depth, rotation, speed) {
        if (size === void 0) { size = 50; }
        if (color === void 0) { color = "#17a2b8"; }
        if (depth === void 0) { depth = 3; }
        if (rotation === void 0) { rotation = true; }
        if (speed === void 0) { speed = 0.8; }
        this.status = status;
        this.size = size;
        this.color = color;
        this.depth = depth;
        this.rotation = rotation;
        this.speed = speed;
    }
    SpinnerModel.prototype.activate = function () {
        this.status = true;
    };
    SpinnerModel.prototype.disable = function () {
        this.status = false;
    };
    return SpinnerModel;
}());
export { SpinnerModel };
//# sourceMappingURL=SpinnerModel.js.map