using Microsoft.Data.SqlClient;
using System.Data;
using ToDo_API.Models;

namespace ToDo_API.Data
{
    public class CategoryRepository
    {
        private IConfiguration configuration;
        public CategoryRepository(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        #region GetAllCategory
        public List<CategoryModel> GetAllCategory()
        {
            var categories = new List<CategoryModel>();
            CategoryModel category = new CategoryModel();

            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Categories_SelectAll";
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                categories.Add(new CategoryModel()
                {
                    CategoryID = Convert.ToInt32(reader["CategoryID"]),
                    UserID = Convert.ToInt32(reader["UserID"]),
                    CategoryName = reader["CategoryName"].ToString(),
                    Description = reader["Description"].ToString(),
                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                    

                });

            }
            return categories;
        }
        #endregion

        #region GetCategoryByID
        public List<CategoryModel> GetCategoryByID(int CategoryID)
        {
            CategoryModel Task = new CategoryModel();
            var category = new List<CategoryModel>();
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Categories_SelectByID";
            command.Parameters.AddWithValue("@CategoryID", CategoryID);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                category.Add(new CategoryModel
                {

                    CategoryID = Convert.ToInt32(reader["CategoryID"]),
                    UserID = Convert.ToInt32(reader["UserID"]),
                    CategoryName = reader["CategoryName"].ToString(),
                    Description = reader["Description"].ToString(),
                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),


                });
            }

            return category;
        }
        #endregion

        #region GetCategoryByUserID
        public List<CategoryModel> GetCategoryByUserID(int UserID)
        {
            CategoryModel Category = new CategoryModel();
            var category = new List<CategoryModel>();
            string connectionString = this.configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Categories_GetCategoryByUser";
            command.Parameters.AddWithValue("@UserID", UserID);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                category.Add(new CategoryModel
                {
                    CategoryID = Convert.ToInt32(reader["CategoryID"]),
                    CategoryName = reader["CategoryName"].ToString(),
                    Description = reader["Description"].ToString(),
                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),



                });
            }

            return category;
        }
        #endregion

        #region AddCategory

        public bool AddCategory(CategoryModel categoryModel)
        {
            SqlConnection conn = new SqlConnection(this.configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_Categories_Insert", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("UserID", categoryModel.UserID);
            cmd.Parameters.AddWithValue("Name", categoryModel.CategoryName);
            cmd.Parameters.AddWithValue("Description", categoryModel.Description);
           




            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        #endregion

        #region UpdateCategory
        public bool UpdateCategory(CategoryModel categoryModel)
        {
            SqlConnection conn = new SqlConnection(this.configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_Categories_Update", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("CategoryID", categoryModel.CategoryID);
            cmd.Parameters.AddWithValue("CategoryName", categoryModel.CategoryName);
            cmd.Parameters.AddWithValue("Description", categoryModel.Description);


            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        #endregion

        #region DeleteCategory
        public bool DeleteCategory(int CategoryID)
        {
            SqlConnection conn = new SqlConnection(this.configuration.GetConnectionString("ConnectionString"));
            conn.Open();
            SqlCommand cmd = new SqlCommand("PR_Categories_Delete", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("CategoryID", CategoryID);

            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion
    }
}
