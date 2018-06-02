module Models {
  import Serializable = General.Serializable;

  interface IWorkoutViewModel {
    id?: number,
    title?: string,
    roundsCount?: number,
    timeToWork?: any,
    timeCap?: any,
    restBetweenExercises?: any,
    restBetweenRounds?: any,
    workoutType: WorkoutType,
    exercisesToDoList: ExerciseViewModel[];
    workoutTypeDisplay?: string;
  }

  export class WorkoutViewModel implements Serializable<WorkoutViewModel>{

    id: number;
    title: string;
    roundsCount?: number;
    timeToWork?: any;
    timeCap?: any;
    restBetweenExercises?: any;
    restBetweenRounds?: any;
    workoutType: WorkoutType;
    workoutTypeDisplay?: string;
    exercisesToDoList: ExerciseViewModel[];

    
    constructor(params?: IWorkoutViewModel) {
      if (params == null) {
        return;
      }
      this.id = params.id;
      this.title = params.title;
      this.roundsCount = params.roundsCount;
      this.timeToWork = params.timeToWork;
      this.timeCap = params.timeCap;
      this.restBetweenExercises = params.restBetweenExercises;
      this.restBetweenRounds = params.restBetweenRounds;
      this.workoutType = params.workoutType;
      this.exercisesToDoList = params.exercisesToDoList;
      this.workoutTypeDisplay = params.workoutTypeDisplay;
    }

    public deserialize(jsonInput): WorkoutViewModel {
      if (jsonInput == null) {
        return null;
      }
      return new WorkoutViewModel({
        id: jsonInput.id,
        title: jsonInput.title,
        roundsCount: jsonInput.roundsCount,
        timeToWork: jsonInput.timeToWork,
        timeCap: jsonInput.timeCap,
        restBetweenExercises: jsonInput.restBetweenExercises,
        restBetweenRounds: jsonInput.restBetweenRounds,
        workoutType: jsonInput.workoutType,
        workoutTypeDisplay: jsonInput.workoutTypeDisplay,
        exercisesToDoList: jsonInput.exercisesToDoList.map(x => new ExerciseViewModel().deserialize(x))
      });
    }
  }
}