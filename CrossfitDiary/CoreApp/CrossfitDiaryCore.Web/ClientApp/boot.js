var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
//import fontawesome from '@fortawesome/fontawesome-free/js/all';
import 'bootstrap';
import './style/app.scss';
import Vue from 'vue';
import { Component } from 'vue-property-decorator';
var BootComponent = /** @class */ (function (_super) {
    __extends(BootComponent, _super);
    function BootComponent() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        // Initial data can be declared as instance properties
        _this.message = 'Hello!';
        return _this;
    }
    // Component methods can be declared as instance methods
    BootComponent.prototype.onClick = function () {
        window.alert(this.message);
    };
    BootComponent = __decorate([
        Component({
            template: '<button @click="onClick">Click!</button>'
        })
    ], BootComponent);
    return BootComponent;
}(Vue));
export default BootComponent;
//# sourceMappingURL=boot.js.map