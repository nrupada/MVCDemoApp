using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleDemo.Data;
using SampleDemo.Entities;
using SampleDemo.Models;

namespace SampleDemo.Controllers
{
    public class ClassroomController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ClassroomController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        /// <summary>
        ///Classrooms_Read
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IActionResult> Classrooms_Read([DataSourceRequest] DataSourceRequest request)
        {
            var roomList =  await _applicationDbContext.Classrooms.ToListAsync();
            return Json(roomList.ToDataSourceResult(request));
        }

        /// <summary>
        /// ManageClassroom
        /// </summary>
        /// <param name="ClassroomId"></param>
        /// <returns></returns>
        // GET: Rooms/ManageClassroom
        public IActionResult ManageClassroom(int? ClassroomId)
        {
            AddClassroomStudentModel addClassroomStudentModel = new AddClassroomStudentModel();
            if (ModelState.IsValid)
            {
                if (ClassroomId > 0)
                {
                    var classroom = _applicationDbContext.Classrooms.Find(ClassroomId);
                    addClassroomStudentModel.ClassroomId = classroom.ClassroomId;
                    addClassroomStudentModel.ClassRoomName = classroom.Name;
                }
            }
            return View(addClassroomStudentModel);
        }

        /// <summary>
        ///ClassroomStudents_Read
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IActionResult ClassroomStudents_Read([DataSourceRequest] DataSourceRequest request, int ClassroomId = 0)
        {
            var studentList = (from r in _applicationDbContext.ClassroomStudents
                            join p in _applicationDbContext.Students on r.StudentId equals p.StudentId
                            where
                            r.ClassroomId == ClassroomId
                            select new Student
                            {
                                StudentId = p.StudentId,
                                Name = p.Name,
                            }).OrderBy(x => x.Name).ToList();

            return Json(studentList.ToDataSourceResult(request));
        }

        // POST: Classroom/AddStudent/5
        [HttpPost]
        public async Task<IActionResult> AddStudent([Bind("ClassroomId,ClassRoomName,StudentName")] AddClassroomStudentModel addClassroomStudentModel)
        {
            if (addClassroomStudentModel != null && addClassroomStudentModel.ClassroomId > 0 && !string.IsNullOrWhiteSpace(addClassroomStudentModel.StudentName))
            {
                try
                {
                    Student student = new Student();
                    student.Name = addClassroomStudentModel.StudentName;
                    _applicationDbContext.Students.Add(student);
                    await _applicationDbContext.SaveChangesAsync();

                    if (student.StudentId > 0)
                    {
                        ClassroomStudent classroomStudent = new ClassroomStudent();
                        classroomStudent.StudentId = student.StudentId;
                        classroomStudent.ClassroomId = addClassroomStudentModel.ClassroomId;
                        _applicationDbContext.ClassroomStudents.Add(classroomStudent);
                        await _applicationDbContext.SaveChangesAsync();
                    }

                    return Json(new { success = true, message = "Record save successfully." });
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!ClassroomExists(addClassroomStudentModel.ClassroomId))
                    {
                        return Json(new { success = false, message = "Class room not found." });
                    }
                    else
                    {
                        throw;
                    }
                }
                catch(Exception ex) {
                
                }
            }

            return Json(new { success = false, message = "Please Enter Valid Data."});
        }

        private bool ClassroomExists(int id)
        {
            return _applicationDbContext.Classrooms.Any(e => e.ClassroomId == id);
        }
    }
}