<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="MongoDbCrud._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>CRUD - MongoDb / Marco Brito</title>

    <!-- Boostrap -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.2.1/css/bootstrap.min.css" integrity="sha384-GJzZqFGwb1QTTN6wy59ffF1BuGJpLSa9DkKMp0DgiMDm4iYMj70gZWKYbI706tWS" crossorigin="anonymous">
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.2.1/js/bootstrap.min.js" integrity="sha384-B0UglyR+jN6CkvvICOB2joaf5I4l3gm9GU6Hc1og6Ls7i6U/mkkaduKaBhlAXv9k" crossorigin="anonymous"></script>
    <style>
        body{
            background-color:#808080;
        }
        .container{
            max-width: 600px !important;
            background-color:#ffffff;
        }
        .container img{
            margin: 0; 
            padding: 0;
        }
       
        .container_titulo{
            text-align:center;
            font-weight:bold;
            font-size:22px;
            padding: 10px 0 5px;
        }
        #footer {
            text-align:center;
            padding-bottom: 10px;
            font-size:12px;
            font-weight:bold;
        }
        #botonera {
            text-align:center;
        }
        .avatar{
            padding-top: 15px;
            text-align: center;
        }
    </style>
</head>
<body>
    
    <div class="container">
        <div class="avatar"><img ID="foto" src="./img/dw.jpg" runat="server" /></div>
        <div class="container_titulo"><spam>Brito</spam> | <spam>CRUD - MongoDB</spam></div>
        <form id="form1" runat="server">
            <div class="form-group">
                <asp:Label ID="lblId" runat="server" Text="Id"></asp:Label>
                <asp:TextBox ID="txtId" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lblResultado" runat="server" CssClass="alert-danger"></asp:Label>
                <asp:Label ID="lblResultadoExitoso" runat="server" CssClass="alert-success"></asp:Label>
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-info" OnClick="btnBuscar_Click" />
            </div>
            <div class="form-group">
                <asp:Label ID="lblNombre" runat="server" Text="Nombre"></asp:Label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lblApellido" runat="server" Text="Apellido"></asp:Label>
                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lblCargo" runat="server" Text="Cargo"></asp:Label>
                <asp:TextBox ID="txtCargo" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group" id="botonera">
                <asp:Button ID="btnInsertar" runat="server" Text="Insertar" CssClass="btn btn-success" OnClick="btnInsertar_Click" />
                <asp:Button ID="btnEditar" runat="server" Text="Editar" CssClass="btn btn-info" OnClick="btnEditar_Click" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnEliminar_Click" />
                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-warning" OnClick="btnLimpiar_Click" />
            </div>
            <div class="form-group" id="footer">
                <asp:Label ID="Label1" runat="server" Text="ASP.NET / C# / MONGODB - Britoxxx"></asp:Label><br />
            </div>
        </form>
    </div>
</body>
</html>
