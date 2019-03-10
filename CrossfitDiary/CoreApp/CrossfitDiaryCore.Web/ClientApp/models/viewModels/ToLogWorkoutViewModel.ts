import Serializable = General.Serializable;
import { WorkoutViewModel } from "./WorkoutViewModel";

interface IToLogWorkoutViewModel {
  id: number;
  crossfitterWorkoutId: number;
  date: string;
  partialRepsFinished: number;
  roundsFinished: number;
  repsToFinishOnCapTime: number;
  selectedWorkoutId: number;
  timePassed: string;
  isEditMode: boolean;

  canBeRemovedByCurrentUser?: boolean;
  workouterName?: string;
  workouterId?: string;
  displayDate?: string;
  workoutViewModel?: WorkoutViewModel;
  comment?: string;
}
export class ToLogWorkoutViewModel implements Serializable<ToLogWorkoutViewModel> {
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

  
  constructor(params?: IToLogWorkoutViewModel) {
    this.displayDate = this.getDefaultDate();
    this.date = this.getDefaultDate();

    if (params == null) {
      return;
    }
    this.id = params.id;
    this.crossfitterWorkoutId = params.crossfitterWorkoutId;
    this.date = params.date;
    this.partialRepsFinished = params.partialRepsFinished;
    this.roundsFinished = params.roundsFinished;
    this.selectedWorkoutId = params.selectedWorkoutId;
    this.timePassed = params.timePassed;
    this.isEditMode = params.isEditMode;

    this.repsToFinishOnCapTime = params.repsToFinishOnCapTime;
    this.canBeRemovedByCurrentUser = params.canBeRemovedByCurrentUser;
    this.workouterName = params.workouterName;
    this.workouterId = params.workouterId;
    this.displayDate = params.displayDate;
    this.workoutViewModel = params.workoutViewModel;
    this.comment = params.comment;
  }

  getDefaultDate = ():string => {
      let date = new Date();
      let result =  ('0' + date.getDate()).slice(-2) + '.'
                  + ('0' + (date.getMonth()+1)).slice(-2) + '.'
                  + date.getFullYear();
                  
      return  result;
  }

  deserialize(jsonInput: any): ToLogWorkoutViewModel {
    if (jsonInput == null) {
      return null as any;
    }

    return new ToLogWorkoutViewModel({
      id: jsonInput.id,
      crossfitterWorkoutId: jsonInput.crossfitterWorkoutId,
      date: jsonInput.date,
      isEditMode: jsonInput.isEditMode,
      partialRepsFinished: jsonInput.partialRepsFinished,
      roundsFinished: jsonInput.roundsFinished,
      selectedWorkoutId: jsonInput.selectedWorkoutId,
      timePassed: jsonInput.timePassed,
      repsToFinishOnCapTime: jsonInput.repsToFinishOnCapTime,
      canBeRemovedByCurrentUser: jsonInput.canBeRemovedByCurrentUser,
      workouterName: jsonInput.workouterName,
      workouterId: jsonInput.workouterId,
      displayDate: jsonInput.displayDate,
      workoutViewModel: new WorkoutViewModel().deserialize(jsonInput.workoutViewModel),
      comment: jsonInput.comment
    });
  }
}
