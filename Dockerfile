# ==== Bắt đầu từ đây ====
# Sử dụng hình ảnh SDK .NET 9.0 để biên dịch ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy các file dự án vào container và khôi phục các gói thư viện
COPY ["Noihay.Web/Noihay.Web.csproj", "Noihay.Web/"]
COPY ["Noihay.Services/Noihay.Services.csproj", "Noihay.Services/"]
COPY ["Noihay.DataAccessLayer/Noihay.DataAccessLayer.csproj", "Noihay.DataAccessLayer/"]
COPY ["Noihay.BusinessObject/Noihay.BusinessObject.csproj", "Noihay.BusinessObject/"]
RUN dotnet restore "Noihay.Web/Noihay.Web.csproj"

# Copy toàn bộ mã nguồn và biên dịch ứng dụng
COPY . .
WORKDIR "/src/Noihay.Web"
RUN dotnet publish "Noihay.Web.csproj" -c Release -o /app/publish

# Tạo hình ảnh chạy cuối cùng (nhẹ hơn, không cần SDK)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .

# Lệnh khởi động ứng dụng
ENTRYPOINT ["dotnet", "Noihay.Web.dll"]
# ==== Kết thúc ====
