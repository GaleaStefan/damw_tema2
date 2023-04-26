using Core.Dtos;
using DataLayer.Entities;
using DataLayer.Repositories;

namespace Core.Services
{
    public class ClassService
    {
        #region Fields
        private readonly ClassRepository _classRepository;
        #endregion

        #region Constructors
        public ClassService(ClassRepository classRepository)
        {
            _classRepository = classRepository;
        }
        #endregion

        #region Public members
        public bool Add(AddClassDto addInformation, int userId)
        {
            var existing = _classRepository.FindByCode(addInformation.Code);
            if (existing == null) return false;

            var newClass = new Class
            {
                Code = addInformation.Code,
                Name = addInformation.Name,
                TeacherId = userId
            };
            _classRepository.Insert(newClass);
            _classRepository.SaveChanges();

            return true;
        }

        public List<ClassViewDto> GetAll()
        {
            return _classRepository.GetAll()
                .Select(c => new ClassViewDto { Code = c.Code, Name = c.Name })
                .ToList();
        }
        #endregion
    }
}
