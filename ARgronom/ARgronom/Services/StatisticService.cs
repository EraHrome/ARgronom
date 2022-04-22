using ARgronom.Contexts;
using ARgronom.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ARgronom.Services
{
    public class StatisticService
    {

        private readonly Context _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private HttpContext Context => _httpContextAccessor.HttpContext;

        public StatisticService(Context context, IHttpContextAccessor httpContextAccessor,
            UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        /// <summary>
        /// Получить все поисковые запросы
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<SearchPlantsModel> GetAllRequests(string userId)
        {
            if (String.IsNullOrEmpty(userId))
            {
                return _context.SearchPlantsModels.ToList();
            }
            else
            {
                return _context.SearchPlantsModels.Where(x => x.UserId == userId).ToList();
            }
        }

        /// <summary>
        /// Добавить 1 просмотр
        /// </summary>
        /// <param name="id"></param>
        public void AddView(int id)
        {
            var viewObject = new ViewObject
            {
                PlantId = id,
                DateTime = DateTime.Now,
                UserId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier)
            };
            _context.ViewObjects.Add(viewObject);
            _context.SaveChanges();
        }

        /// <summary>
        /// Получить самые популярные поисковые тэги по убыванию
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public Dictionary<string, int> GetPopularSearchTags(int count)
        {
            var tags = _context.SearchPlantsModels.Where(x => !String.IsNullOrEmpty(x.Query)).Select(x => x.Query).ToList();
            var tagsDict = new Dictionary<string, int>();

            if (tags.Any())
            {

                foreach (var tag in tags)
                {
                    if (tagsDict.ContainsKey(tag))
                    {
                        tagsDict[tag]++;
                    }
                    else
                    {
                        tagsDict.Add(tag, 1);
                    }
                }

                var orderedDictionary = new Dictionary<string, int>();
                var tagsCount = tagsDict.Count;
                for (int i = 0; i < tagsCount && i < count; i++)
                {
                    var maxTag = tagsDict.First(x => x.Value == tagsDict.Max(x => x.Value));
                    orderedDictionary.Add(maxTag.Key, maxTag.Value);
                    tagsDict.Remove(maxTag.Key);
                }

                return orderedDictionary;

            }

            return tagsDict;
        }

        /// <summary>
        /// Добавить элемент статистики после поиска
        /// </summary>
        /// <param name="search"></param>
        public void AddStatistic(SearchPlantsModel search, int foundedCount)
        {
            if (search != null)
            {
                var notEmpty = false;

                var statisticObject = new SearchPlantsModel();

                if (!String.IsNullOrEmpty(search.Query))
                {
                    notEmpty = true;
                    statisticObject.Query = search.Query;
                }

                if (notEmpty)
                {
                    var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                    statisticObject.UserId = userId;

                    _context.SearchPlantsModels.Add(statisticObject);
                    _context.SaveChanges();
                }

            }
        }

    }
}
