import Deserializable = General.Deserializable;
import {WorkoutViewModel} from "./WorkoutViewModel";
import {WodSubType} from "./WodSubType";
export class ToLogWorkoutViewModel implements Deserializable {
    id: number = 0;
    crossfitterWorkoutId: number = 0;
    date: string = new Date().toLocaleDateString();
    partialRepsFinished?: number;
    roundsFinished?: number;
    repsToFinishOnCapTime?: number;
    selectedWorkoutId: number = 0;
    timePassed: string = null;
    isEditMode: boolean = false;

    canBeRemovedByCurrentUser?: boolean;
    workouterName?: string;
    workouterId?: string;
    displayDate?: string;// default value for new model
    // displayDate?: string = new Date().toLocaleDateString(); // default value for new model

    workoutViewModel?: WorkoutViewModel;
    comment?: string;
    wodSubType: WodSubType;


    constructor(input?: any) {
        this.displayDate = this.getDefaultDate();
        this.date = this.getDefaultDate();
        this.wodSubType = WodSubType.Skill;

        if (input == null) {
            return;
        }
        // seems that it doesn't used
        Object.assign(this, input);
    }

    getDefaultDate = (): string => {
        let date = new Date();
        return ('0' + date.getDate()).slice(-2) + '.'
            + ('0' + (date.getMonth() + 1)).slice(-2) + '.'
            + date.getFullYear();
    };

    deserialize(jsonInput: any): this {
        if (jsonInput == null) {
            return null as any;
        }
        Object.assign(this, jsonInput);
        this.workoutViewModel = new WorkoutViewModel().deserialize(jsonInput.workoutViewModel);
        return this;
    }
}
