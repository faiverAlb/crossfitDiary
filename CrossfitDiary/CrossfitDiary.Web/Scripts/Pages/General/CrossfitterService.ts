module General {
  import WorkoutViewModel = Models.WorkoutViewModel;
  import ToLogWorkoutViewModel = Models.ToLogWorkoutViewModel;
  import ExerciseViewModel = Models.ExerciseViewModel;
  import PersonExerciseRecord = Models.PersonExerciseRecord;

  export class CrossfitterService extends BaseService {

    constructor(public readonly  pathToApp: string, public isDataLoading: KnockoutObservable<Boolean>) {
      super();
    }

    public createAndLogWorkout = (model: { newWorkoutViewModel: WorkoutViewModel, logWorkoutViewModel: ToLogWorkoutViewModel }) => {
      this.isDataLoading(true);
      return this.post(this.pathToApp + "api/createAndLogNewWorkout", model);
    };

    public getAvailableWorkouts = (): Q.Promise<WorkoutViewModel[]> => {
      this.isDataLoading(true);
      return this.get<WorkoutViewModel[]>(this.pathToApp + "api/getAvailableWorkouts")
        .then((jsonData) => {
          return jsonData.map(x => new WorkoutViewModel().deserialize(x));
        })
        .finally(() => { this.isDataLoading(false); });
    };

    public getAllCrossfittersWorkouts = (userId:string,exerciseId?:number): Q.Promise<ToLogWorkoutViewModel[]> => {
      this.isDataLoading(true);
      return this.get<ToLogWorkoutViewModel[]>(this.pathToApp + `api/getAllCrossfittersWorkouts/users/${userId}/exercises/${exerciseId}`)
        .then((jsonData) => {
          return jsonData.map(x => new ToLogWorkoutViewModel().deserialize(x));
        })
        .finally(() => { this.isDataLoading(false); });
    };

    public getExercises = (): Q.Promise<ExerciseViewModel[]> => {
      this.isDataLoading(true);
      return this.get<ExerciseViewModel[]>(this.pathToApp + "api/getExercises")
        .then((jsonData) => {
          return jsonData.map(x => new ExerciseViewModel().deserialize(x));
        })
        .finally(() => { this.isDataLoading(false); });
    };

    public getStatisticalExercises = (): Q.Promise<ExerciseViewModel[]> => {
      this.isDataLoading(true);
      return this.get<ExerciseViewModel[]>(this.pathToApp + "api/getStatisticalExercises")
        .then((jsonData) => {
          return jsonData.map(x => new ExerciseViewModel().deserialize(x));
        })
        .finally(() => { this.isDataLoading(false); });
    };

    public getPersonExerciseMaximumWeight = (exerciseId: number) => {
      this.isDataLoading(true);
      return this.get<PersonExerciseRecord[]>(this.pathToApp + `api/exercises/${exerciseId}/personMaximum`)
        .then((jsonData) => {
          return jsonData.map(x => new PersonExerciseRecord().deserialize(x));
        })
        .finally(() => { this.isDataLoading(false); });
    };

    public getAllPersonsExerciseMaximumWeights = (exerciseId: number) => {
      this.isDataLoading(true);
      return this.get<PersonExerciseRecord[]>(this.pathToApp + `api/exercises/${exerciseId}/allPersonsMaximums`)
        .then((jsonData) => {
          return jsonData.map(x => new PersonExerciseRecord().deserialize(x));
        })
        .finally(() => { this.isDataLoading(false); });
    };

    public removeWorkout = (crossfitterWorkoutId) => {
      this.isDataLoading(true);
      return this.delete(`api/removeWorkout/${crossfitterWorkoutId}`).finally(() => { this.isDataLoading(false); });
    };

    public getPersonLoggingInfo = (preselectedCrossfitterWorkoutId: number): Q.Promise<ToLogWorkoutViewModel> => {
      this.isDataLoading(true);
      return this.get<ToLogWorkoutViewModel>(this.pathToApp + `api/getPersonLoggingInfo/${preselectedCrossfitterWorkoutId}`)
        .then((jsonData) => {
          return new ToLogWorkoutViewModel().deserialize(jsonData);
        })
        .finally(() => {
          this.isDataLoading(false);
        });
    };
  }
}