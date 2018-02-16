//module Models {
//
//  export class SimpleRoutine {
//    exercise: any;
//    exerciseMeasures: KnockoutObservableArray<any>;
//
//    constructor(exerciseModel, isFieldsRequired = null) {
//      this.exercise = exerciseModel;
//      this.exerciseMeasures = ko.observableArray([]);
//      for (let i = 0; i < this.exercise.exerciseMeasures.length; i++) {
//        const exerciseMeasureType = this.exercise.exerciseMeasures[i].exerciseMeasureType;
//
//        const exerciseMeasureTypeValue = new ExerciseMeasureTypeViewModelObservable(exerciseMeasureType);
//
//        this.exerciseMeasures.push(exerciseMeasureTypeValue);
//      }
//
//    }
//
//
//    public toJSON = () => {
//      const model = {
//        exerciseMeasures: [],
//        id: this.exercise.id,
//        title: this.exercise.title,
//        isAlternative: this.exercise.isAlternative
//      };
//      $.each(this.exerciseMeasures(),
//        (index, measure) => {
//          model.exerciseMeasures.push(measure.toJSON());
//        });
//      return model;
//
//    };
//  }
//}