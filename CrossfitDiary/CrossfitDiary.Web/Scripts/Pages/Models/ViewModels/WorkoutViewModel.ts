module Models {
  import Serializable = General.Serializable;

  interface IWorkoutViewModel {
    id?: number,
    title?: string,
    detailedTitle?: string,
    roundsCount?: number,
    timeToWork?: any,
    restBetweenExercises?: any,
    restBetweenRounds?: any,
    workoutType: WorkoutType,
    exercisesToDoList: ExerciseViewModel[];
  }

  export class WorkoutViewModel implements Serializable<WorkoutViewModel>{

    id: number;
    title: string;
    detailedTitle: string;
    roundsCount?: number;
    timeToWork?: any;
    restBetweenExercises?: any;
    restBetweenRounds?: any;
    workoutType: WorkoutType;
    exercisesToDoList: ExerciseViewModel[];

    
    constructor(params?: IWorkoutViewModel) {
      if (params == null) {
        return;
      }
      this.id = params.id;
      this.title = params.title;
      this.detailedTitle = params.detailedTitle;
      this.roundsCount = params.roundsCount;
      this.timeToWork = params.timeToWork;
      this.restBetweenExercises = params.restBetweenExercises;
      this.restBetweenRounds = params.restBetweenRounds;
      this.workoutType = params.workoutType;
      this.exercisesToDoList = params.exercisesToDoList;
    }

    public deserialize(jsonInput): WorkoutViewModel {
      if (jsonInput == null) {
        return null;
      }
      return new WorkoutViewModel({
        id: jsonInput.id,
        title: jsonInput.title,
        detailedTitle: jsonInput.detailedTitle,
        roundsCount: jsonInput.roundsCount,
        timeToWork: jsonInput.timeToWork,
        restBetweenExercises: jsonInput.restBetweenExercises,
        restBetweenRounds: jsonInput.restBetweenRounds,
        workoutType: jsonInput.workoutType,
        exercisesToDoList: jsonInput.exercisesToDoList.map(x => new ExerciseViewModel().deserialize(x))
      });
    }
  }
}