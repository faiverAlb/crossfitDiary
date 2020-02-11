import {WorkoutType} from "./WorkoutType";
import {ExerciseViewModel} from "./ExerciseViewModel";
import Deserializable = General.Deserializable;

export enum PlanningWorkoutLevel {
    Scaled = 0,
    Rx = 1,
    RxPlus = 2
}

export class WorkoutViewModel implements Deserializable {
    id: number = 0;
    title: string = "";
    roundsCount?: number;
    timeToWork?: any;
    timeCap?: any;
    restBetweenExercises?: any;
    restBetweenRounds?: string;
    workoutType: WorkoutType = WorkoutType.ForTime;
    workoutTypeDisplay?: string;
    exercisesToDoList: ExerciseViewModel[] = [];
    children: WorkoutViewModel[] = [];
    isInnerWorkout: boolean = false;
    // planningWorkoutLevel: PlanningWorkoutLevel;
    // displayPlanDate?: string; // default value for new model;
    comment?: string;
    resultsCount: number;
    canSeeLeaderboard: boolean;
    haveCollapsedVersion: boolean = false;
    canShowCountOnce: boolean = true;
    asNonBreakingSet: boolean = false;
    findMaxWeight: boolean = false;

    groupedDictionary: {} = {};
    oneTimeSchema: any = {};
    // subTypeClass: string = "";
    // wodSubType: WodSubType;

    // getDefaultDate = (): string => {
    //     let date: Date = new Date();
    //     let result: string = ("0" + date.getDate()).slice(-2) + "." + ("0" + (date.getMonth() + 1)).slice(-2) + "." + date.getFullYear();
    //
    //     return result;
    // };
    public IsForTime = () => {
        return this.workoutType === WorkoutType.ForTime;
    };

    public IsAMRAP = () => {
        return this.workoutType === WorkoutType.AMRAP || this.workoutType === WorkoutType.AMRAPN;
    };
    public IsEmoms = () => {
        return this.workoutType === WorkoutType.E2MOM || this.workoutType === WorkoutType.EMOM;
    };

    public IsHaveCapTime = () => {
        return this.workoutType === WorkoutType.ForTime || this.workoutType === WorkoutType.ForTimeManyInners;
    };
    public IsFindMaxWeight = () => {
        return this.workoutType === WorkoutType.NotForTime && this.findMaxWeight == true;
    };

    private getHaveCollapsedVersion = (exercisedToUse: ExerciseViewModel[], distinctFirst: ExerciseViewModel[]): boolean => {
        let canBeCollapsed: boolean = true;
        for (let i = 0; i < exercisedToUse.length; i++) {
            if (canBeCollapsed === false) {
                break;
            }

            for (let j = 0; j < distinctFirst.length; j++) {
                const distinctItem: ExerciseViewModel = distinctFirst[j];
                if (i + j < exercisedToUse.length && exercisedToUse[i + j].id !== distinctItem.id) {
                    canBeCollapsed = false;
                    break;
                }
            }
            i = i + distinctFirst.length - 1;
        }
        return canBeCollapsed;
    };

    private getDistinctItems = (exercisedToUse: ExerciseViewModel[]): ExerciseViewModel[] => {
        let distinctFirst: ExerciseViewModel[] = [];
        for (let index: number = 0; index < exercisedToUse.length; index++) {
            const element: ExerciseViewModel = exercisedToUse[index];
            let foundInDistinct: ExerciseViewModel = distinctFirst.find(x => {
                return x.id === element.id;
            });
            if (!foundInDistinct) {
                distinctFirst.push(element);
            } else {
                break;
            }
        }
        return distinctFirst;
    };

    private groupExercises = (exercisedToUse: ExerciseViewModel[]) => {
        let groupedDictionary: {} = {};
        for (let i: number = 0; i < exercisedToUse.length; i++) {
            const exercise: ExerciseViewModel = exercisedToUse[i];
            if (groupedDictionary[exercise.id]) {
                groupedDictionary[exercise.id].push(exercise);
            } else {
                groupedDictionary[exercise.id] = [];
                groupedDictionary[exercise.id].push(exercise);
            }
        }
        return groupedDictionary;
    };


    private getCanShowCountOnce = (exercisedToUse: ExerciseViewModel[], distinctLength: number) => {
        let canShowCountOnce: boolean = true;
        for (let i: number = 0; i < exercisedToUse.length; i++) {
            if (canShowCountOnce === false) {
                break;
            }
            for (let j: number = 0; j < distinctLength; j++) {
                if (i + j + 1 >= exercisedToUse.length || i + j + 1 >= distinctLength) {
                    break;
                }
                let currentExericseToUse = exercisedToUse[i + j];
                let nextExericseToUse = exercisedToUse[i + j + 1];
                if (currentExericseToUse.haveSameCountAndCalories(nextExericseToUse) === false) {
                    canShowCountOnce = false;
                    break;
                }
            }
            i = i + distinctLength;
        }
        return canShowCountOnce;
    };

    private tryCollapseWorkout(): void {
        let exercisedToUse: ExerciseViewModel[] = this.exercisesToDoList;
        let distinctExercises: ExerciseViewModel[] = this.getDistinctItems(exercisedToUse);
        if (distinctExercises.length === 0) {
            return;
        }
        if (exercisedToUse.length === distinctExercises.length) {
            return;
        }
        if (exercisedToUse.length % distinctExercises.length !== 0) {
            return;
        }
        this.haveCollapsedVersion = this.getHaveCollapsedVersion(exercisedToUse, distinctExercises);
        this.haveCollapsedVersion =  false; //todo: investigate and fix
        if (this.haveCollapsedVersion === false) {
            return;
        }
        this.groupedDictionary = this.groupExercises(exercisedToUse);
        let distinctLength: number = distinctExercises.length;
        this.canShowCountOnce = this.getCanShowCountOnce(exercisedToUse, distinctLength);
        if (this.canShowCountOnce) {
            this.oneTimeSchema.schemaString = "";
            let firstExerciseArray: ExerciseViewModel[] = this.groupedDictionary[exercisedToUse[0].id];
            for (let index: number = 0; index < firstExerciseArray.length; index++) {
                const element: ExerciseViewModel = firstExerciseArray[index];
                if (element.count) {
                    this.oneTimeSchema.schemaString += element.count;
                }
                if (element.calories) {
                    this.oneTimeSchema.schemaString += element.calories;
                }
                if (index + 1 < firstExerciseArray.length) {
                    this.oneTimeSchema.schemaString += "-";
                }
            }
        }
    }

    constructor(input?: any) {
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
        this.children = input.children.map((x): WorkoutViewModel => new WorkoutViewModel().deserialize(x));
        this.exercisesToDoList = input.exercisesToDoList.map((x): ExerciseViewModel => new ExerciseViewModel().deserialize(x));
        this.tryCollapseWorkout();
        return this;
    }
}
