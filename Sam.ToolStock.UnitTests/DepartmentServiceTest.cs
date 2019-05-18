using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Moq;
using NUnit.Framework;
using Sam.ToolStock.DataProvider.Interfaces;
using Sam.ToolStock.DataProvider.Models;
using Sam.ToolStock.Logic.Services;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.UnitTests
{
    [TestFixture]
    class DepartmentServiceTest
    {
        private MapperConfiguration _mapConfig;
        private IMapper _mapper;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IRepository<DepartmentModel>> _departmentRepository;

        [SetUp]
        public void Init()
        {
            _mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(Assembly.GetAssembly(typeof(DepartmentService)));
            });
            _mapper = new Mapper(_mapConfig);

            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _departmentRepository = new Mock<IRepository<DepartmentModel>>();
        }

        [Test]
        public void Create_PassesNewDepartmentToUnitOfWork_CallsCreateMethodOfDepartmentRepository()
        {

            var newDepartment = new DepartmentViewModel
            {
                Name = "New department"
            };

            _unitOfWorkMock.Setup(m => m.DepartmentRepository).Returns(_departmentRepository.Object);

            var departmentService = new DepartmentService(_unitOfWorkMock.Object, _mapper);

            departmentService.Create(newDepartment);

            _departmentRepository.Verify(m => m.Create(It.IsAny<DepartmentModel>()), Times.Once);
        }

        [Test]
        public void Create_PassesNewDepartmentToUnitOfWork_DepartmentHasId()
        {

            var newDepartment = new DepartmentViewModel
            {
                Name = "New department"
            };

            _unitOfWorkMock.Setup(m => m.DepartmentRepository).Returns(_departmentRepository.Object);

            var departmentService = new DepartmentService(_unitOfWorkMock.Object, _mapper);

            departmentService.Create(newDepartment);

            Assert.That(newDepartment.Id, Is.Not.Null);
            Assert.That(newDepartment.Id, Is.Not.Empty);
        }

        [Test]
        public void Create_PassesNewDepartmentToUnitOfWork_DepartmentHasValidId()
        {

            var newDepartment = new DepartmentViewModel
            {
                Name = "New department"
            };

            _unitOfWorkMock.Setup(m => m.DepartmentRepository).Returns(_departmentRepository.Object);

            var departmentService = new DepartmentService(_unitOfWorkMock.Object, _mapper);

            departmentService.Create(newDepartment);

            Assert.That(newDepartment.Id, Does.Match(@"(?im)^[{(]?[0-9A-F]{8}[-]?(?:[0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$"));
        }

        [Test]
        public void GetAll_GetsAllDepartmentsFromDb_CallsGetAllMethodOfDepartmentRepository()
        {
            _unitOfWorkMock.Setup(m => m.DepartmentRepository).Returns(_departmentRepository.Object);

            var departmentService = new DepartmentService(_unitOfWorkMock.Object, _mapper);

            departmentService.GetAll();

            _departmentRepository.Verify(m => m.GetAll(), Times.Once);
        }

        [Test]
        public void GetAll_GetsAllDepartmentsFromDb_GetsEnumerationOfDepartments()
        {
            var departmentModels = (IEnumerable<DepartmentModel>)new List<DepartmentModel>
            {
                new DepartmentModel {Id = Guid.NewGuid().ToString(), Name = "Some department"},
                new DepartmentModel {Id = Guid.NewGuid().ToString(), Name = "Some department"}
            };

            _unitOfWorkMock.Setup(m => m.DepartmentRepository).Returns(_departmentRepository.Object);
            _departmentRepository.Setup(m => m.GetAll()).Returns(departmentModels);

            var departmentService = new DepartmentService(_unitOfWorkMock.Object, _mapper);

            var result = departmentService.GetAll();

            Assert.That(result, Is.InstanceOf<IEnumerable<DepartmentViewModel>>());
        }

        [Test]
        public void GetAll_GetsAllDepartmentsWithoutDeletedFromDb_CallsGetWhereMethodOdRepository()
        {
            var departmentModels = (IEnumerable<DepartmentModel>)new List<DepartmentModel>
            {
                new DepartmentModel {Id = Guid.NewGuid().ToString(), Name = "Some department"},
                new DepartmentModel {Id = Guid.NewGuid().ToString(), Name = "Some department", IsDeleted = true},
                new DepartmentModel {Id = Guid.NewGuid().ToString(), Name = "Some department"},
                new DepartmentModel {Id = Guid.NewGuid().ToString(), Name = "Some department", IsDeleted = true},
                new DepartmentModel {Id = Guid.NewGuid().ToString(), Name = "Some department", IsDeleted = true},
                new DepartmentModel {Id = Guid.NewGuid().ToString(), Name = "Some department"},
                new DepartmentModel {Id = Guid.NewGuid().ToString(), Name = "Some department"}
            };

            _unitOfWorkMock.Setup(m => m.DepartmentRepository).Returns(_departmentRepository.Object);
            _departmentRepository.Setup(m => m.GetWhere(It.IsAny<Func<DepartmentModel, bool>>()))
                .Returns(departmentModels.Where(d => d.IsDeleted == false));

            var departmentService = new DepartmentService(_unitOfWorkMock.Object, _mapper);

            departmentService.GetAll(false);

            _departmentRepository.Verify(m => m.GetWhere(It.IsAny<Func<DepartmentModel, bool>>()), Times.Once);
        }

        [Test]
        public void GetAll_GetsAllDepartmentsWithDeletedFromDb_DoesNotCallGetWhereMethodOdRepository()
        {
            var departmentModels = (IEnumerable<DepartmentModel>)new List<DepartmentModel>
            {
                new DepartmentModel {Id = Guid.NewGuid().ToString(), Name = "Some department"},
                new DepartmentModel {Id = Guid.NewGuid().ToString(), Name = "Some department", IsDeleted = true},
                new DepartmentModel {Id = Guid.NewGuid().ToString(), Name = "Some department"},
                new DepartmentModel {Id = Guid.NewGuid().ToString(), Name = "Some department", IsDeleted = true},
                new DepartmentModel {Id = Guid.NewGuid().ToString(), Name = "Some department", IsDeleted = true},
                new DepartmentModel {Id = Guid.NewGuid().ToString(), Name = "Some department"},
                new DepartmentModel {Id = Guid.NewGuid().ToString(), Name = "Some department"}
            };

            _unitOfWorkMock.Setup(m => m.DepartmentRepository).Returns(_departmentRepository.Object);
            _departmentRepository.Setup(m => m.GetWhere(It.IsAny<Func<DepartmentModel, bool>>()))
                .Returns(departmentModels.Where(d => d.IsDeleted == false));

            var departmentService = new DepartmentService(_unitOfWorkMock.Object, _mapper);

            departmentService.GetAll(true);

            _departmentRepository.Verify(m => m.GetWhere(It.IsAny<Func<DepartmentModel, bool>>()), Times.Never);
        }

        [Test]
        public void GetAll_GetsAllDepartmentsWithoutDeletedFromDb_GetsCorrectNumberOfDepartments()
        {
            var departmentModels = (IEnumerable<DepartmentModel>)new List<DepartmentModel>
            {
                new DepartmentModel {Id = Guid.NewGuid().ToString(), Name = "Some department"},
                new DepartmentModel {Id = Guid.NewGuid().ToString(), Name = "Some department", IsDeleted = true},
                new DepartmentModel {Id = Guid.NewGuid().ToString(), Name = "Some department"},
                new DepartmentModel {Id = Guid.NewGuid().ToString(), Name = "Some department", IsDeleted = true},
                new DepartmentModel {Id = Guid.NewGuid().ToString(), Name = "Some department", IsDeleted = true},
                new DepartmentModel {Id = Guid.NewGuid().ToString(), Name = "Some department"},
                new DepartmentModel {Id = Guid.NewGuid().ToString(), Name = "Some department"}
            };

            _unitOfWorkMock.Setup(m => m.DepartmentRepository).Returns(_departmentRepository.Object);
            _departmentRepository.Setup(m => m.GetWhere(It.IsAny<Func<DepartmentModel, bool>>()))
                .Returns(departmentModels.Where(d => d.IsDeleted == false));

            var departmentService = new DepartmentService(_unitOfWorkMock.Object, _mapper);

            var result = departmentService.GetAll(false).Count();

            Assert.That(result, Is.EqualTo(departmentModels.Count(d => d.IsDeleted == false)));
        }
    }
}
