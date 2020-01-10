import Deserializable = General.Deserializable;
import {PlanningWorkoutLevel, WorkoutViewModel} from "./WorkoutViewModel";
import {WodSubType} from "./WodSubType";

export class PlanningWorkoutViewModel implements Deserializable {
    id: number = 0;
    planningWorkoutLevel: PlanningWorkoutLevel;
    wodSubType: WodSubType;
    displayPlanDate?: string; // default value for new model;
    workoutViewModel: WorkoutViewModel;
    subTypeClass: string = "";

    get workoutSubTypeDisplayValue() {
        this.subTypeClass = this.getSubTypeClass();
        switch (this.wodSubType) {
            case WodSubType.Skill:
                return "Skill";
            case WodSubType.Wod:
                return "WOD";
            case WodSubType.AccessoryWork:
                return "Accessory";
        }


    }


    get planningLevelDisplayValue() {
        switch (this.planningWorkoutLevel) {
            case PlanningWorkoutLevel.Scaled:
                return "Scaled";
            case PlanningWorkoutLevel.Rx:
                return "Rx";
            case PlanningWorkoutLevel.RxPlus:
                return "Rx+";
        }
    }


    getSubTypeClass() {
        switch (this.wodSubType) {
            case WodSubType.Skill:
                return 'bg-info text-white';
            case WodSubType.Wod:
                return 'bg-danger text-white';
            case WodSubType.AccessoryWork:
                return 'bg-warning text-white';
        }

    }

    getDefaultDate = (): string => {
        let date: Date = new Date();
        let result: string = ("0" + date.getDate()).slice(-2) + "." + ("0" + (date.getMonth() + 1)).slice(-2) + "." + date.getFullYear();

        return result;
    };


    constructor(input?: any) {
        this.displayPlanDate = this.getDefaultDate();
        this.wodSubType = WodSubType.Skill;
        if (input == null) {
            return;
        }
        Object.assign(this, input);
    }

    public deserialize(input: any): this {
        if (input == null) {
            return;
        }
        Object.assign(this, input);
        this.workoutViewModel = new WorkoutViewModel().deserialize(input.workoutViewModel);
        return this;
    }
}
