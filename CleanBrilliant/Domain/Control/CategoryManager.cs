using CleanBrilliant.Domain.BoundaryInterface;
using CleanBrilliant.Domain.Entity;
using CleanBrilliant.DTO;
using System.Data;

namespace CleanBrilliant.Domain.Control
{
    public class CategoryManager
    {
        private readonly ICategoryGateway _gateway;

        public CategoryManager(ICategoryGateway gateway)
        {
            _gateway = gateway;
        }

        // + getCategoryById(categoryId: Int): Category
        public async Task<Category> GetCategoryById(int categoryId)
        {
            var ds = await _gateway.GetById(categoryId);

            var table = ds.Tables["category"];
            if (table == null || table.Rows.Count == 0)
                throw new KeyNotFoundException($"Category not found: {categoryId}");

            var row = table.Rows[0];

            return new Category(
                (int)row["category_id"],
                (string)row["name"],
                row["description"] == DBNull.Value ? "" : (string)row["description"],
                (bool)row["activity_status"]
            );
        }

        // + categoryExists(categoryId: Int): Boolean
        public async Task<bool> CategoryExists(int categoryId)
        {
            var ds = await _gateway.GetById(categoryId);
            var table = ds.Tables["category"];
            if (table == null || table.Rows.Count == 0)
                return false;

            return true;
        }

        // + listAllCategory(): List<CategoryDTO>
        public async Task<List<CategoryDTO>> ListAllCategory()
        {
            var ds = await _gateway.GetAll();

            var table = ds.Tables["category"];

            if (table == null || table.Rows.Count == 0)
                return new List<CategoryDTO>();

            var list = new List<CategoryDTO>(table.Rows.Count);

            foreach (DataRow row in table.Rows)
            {
                list.Add(new CategoryDTO
                {
                    Id = (int)row["category_id"],
                    Name = (string)row["name"],
                    Description = row["description"] == DBNull.Value ? "" : (string)row["description"],
                    ActivityStatus = (bool)row["activity_status"]
                });
            }

            return list;
        }
    }
}
