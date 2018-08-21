//module Models {
//  export class ExerciseViewModelObservable {
//    _exerciseMeasures: ExerciseMeasureViewModelObservable[];
//    _isDoUnbroken: KnockoutObservable<boolean>;
//
//    constructor(public model: ExerciseViewModel, personMaximums: PersonExerciseRecord[]) {
//      let personMaximumWeight: number = this.getPersonMaximum(model.id, personMaximums);
//      this._exerciseMeasures = model.exerciseMeasures.map(item => new ExerciseMeasureViewModelObservable(item, personMaximumWeight));
//
//      this._isDoUnbroken = ko.observable(model.isDoUnbroken);
//    }
//
//
//    private getPersonMaximum = (exerciseId: number, personMaximums: PersonExerciseRecord[]): number => {
//      for (let i = 0; i < personMaximums.length; i++) {
//        if (personMaximums[i].exerciseId === exerciseId) {
//          return personMaximums[i].maximumWeightValue;
//        }
//      }
//      return null;
//    }
//
//    private changeUnbrokenState = () => {
//      this._isDoUnbroken(!this._isDoUnbroken());
//    };
//
//    public toPlainObject = (): ExerciseViewModel => {
//      let plainExercise = new ExerciseViewModel(
//        {
//          id: this.model.id,
//          title: this.model.title,
//          exerciseMeasures: this._exerciseMeasures.map(item => item.toPlainObject()),
//          isAlternative: this.model.isAlternative,
//          isDoUnbroken: this._isDoUnbroken()
//        });
//      return plainExercise;
//    };
//  }
//}