INSERT INTO [ExerciseMeasure]
           ([CreatedUtc]
           ,[ExerciseId]
           ,[ExerciseMeasureTypeId])
SELECT GetDate() as CreatedUTC, Id as [ExerciseId], 8 as 'ExerciseMeasureTypeId'
FROM Exercise WHERE Exercise.Id IN ( 
SELECT [ExerciseId]
  FROM [ExerciseMeasure]
  WHERE ExerciseMeasureTypeId = 3)