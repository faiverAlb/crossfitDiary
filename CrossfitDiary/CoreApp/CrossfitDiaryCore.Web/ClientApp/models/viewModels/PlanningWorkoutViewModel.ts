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
    isPrivatePlanning:boolean = false;





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
