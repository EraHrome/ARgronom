using ARgronom.Contexts;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace ARgronom.Services
{
    public class SearchService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Context _context;
        private readonly IMapper _mapper;
        private HttpContext Context => _httpContextAccessor.HttpContext;

        public SearchService(Context context, IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// Получить список всех категорий
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllCategories()
        {
            var categories = _context.Plants.Select(x => x.Category).Distinct().ToList();
            return categories;
        }

    }
}
