module Models {
  import Serializable = General.Serializable;

  interface IPersonExerciseRecord {
    personName: string;
    maximumWeight: string;
    date: string; 
    workoutTitle: string;
    positionBetweenOthers: number;
    isItMe: boolean;
    exerciseId: number;
    maximumWeightValue?: number;
  }

  export class PersonExerciseRecord implements Serializable<PersonExerciseRecord>{

    personName: string;
    maximumWeight: string;
    maximumWeightValue?: number;
    date: string;
    workoutTitle: string;
    positionBetweenOthers: number;
    isItMe: boolean;
    exerciseId: number;

    constructor(params?:IPersonExerciseRecord) {
      if (params == null) {
        return;
      }
      this.personName = params.personName;
      this.maximumWeight = params.maximumWeight;
      this.date = params.date;
      this.workoutTitle = params.workoutTitle;
      this.positionBetweenOthers = params.positionBetweenOthers;
      this.isItMe = params.isItMe;
      this.exerciseId = params.exerciseId;
      this.maximumWeightValue = params.maximumWeightValue;
    }

    deserialize(input): PersonExerciseRecord {
      if (input == null) {
        return null;
      }
      
      return new PersonExerciseRecord({
        personName: input.personName,
        maximumWeight: input.maximumWeight,
        date: input.date,
        workoutTitle: input.workoutTitle,
        positionBetweenOthers: input.positionBetweenOthers,
        isItMe: input.isItMe,
        exerciseId: input.exerciseId,
        maximumWeightValue: input.maximumWeightValue,
      });
    }

  }
}