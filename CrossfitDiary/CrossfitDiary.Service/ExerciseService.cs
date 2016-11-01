﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrossfitDiary.DAL.EF.Infrastructure;
using CrossfitDiary.DAL.EF.Repositories;
using CrossfitDiary.Model;

namespace CrossfitDiary.Service
{

    public interface IExerciseService
    {
        IEnumerable<Exercise> GetExercises(string title = null);
        Exercise GetExercise(int id);

        void CreateExercise(Exercise exercise);
        void SaveExercise();
    }
    public class ExerciseService: IExerciseService
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IUnitOfWork _unitOfWork;


        public ExerciseService(IExerciseRepository exerciseRepository, IUnitOfWork unitOfWork)
        {
            _exerciseRepository = exerciseRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Exercise> GetExercises(string title = null)
        {
            return string.IsNullOrEmpty(title) ? _exerciseRepository.GetAll() : _exerciseRepository.GetMany(x => x.Title == title);
        }

        public Exercise GetExercise(int id)
        {
            return _exerciseRepository.GetById(id);
        }

        public void CreateExercise(Exercise exercise)
        {
            _exerciseRepository.Add(exercise);
        }

        public void SaveExercise()
        {
            _unitOfWork.Commit();
        }
    }
}
