using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODT_Repository.Entity;
using ODT_Model.DTO.Request;
using ODT_Model.DTO.Response;

namespace ODT_Model.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            

            #region Subcription
            CreateMap<CreateSubcriptionRequest, Subcription>().ReverseMap();
            CreateMap<UpdateSubcriptionRequest, Subcription>().ReverseMap();
            CreateMap<SubcriptionResponse, Subcription>().ReverseMap();
            #endregion



            #region StudentSubcription 
            CreateMap<CreateStudentSubcriptionRequest, StudentSubcription>().ReverseMap();
            CreateMap<UpdateStudentSubcriptionRequest, StudentSubcription>().ReverseMap();
            CreateMap<StudentSubcriptionResponse, StudentSubcription>().ReverseMap();
            #endregion

        }
    }
}
