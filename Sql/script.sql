CREATE TABLE Cliente(
	ruc CHAR(11) PRIMARY KEY,
	razon_social VARCHAR2(100),
	telefono VARCHAR2(20),
	contacto VARCHAR2(60)
);

CREATE TABLE Sede(
    id_sede INT PRIMARY KEY,
	pais VARCHAR2(40),
	departamento VARCHAR2(100),
	direccion VARCHAR2(160),
	telefono VARCHAR2(20),
	contacto VARCHAR2(60)
);



CREATE TABLE Sede_x_Cliente(
    id_sede_cliente INT PRIMARY KEY,
	id_sede INT,
	ruc CHAR(11),
	FOREIGN KEY(id_sede) REFERENCES Sede(id_sede),
	FOREIGN KEY(ruc) REFERENCES Cliente(ruc)
);

INSERT INTO SEDE VALUES(1 , 'PERU','LIMA','Av. Venezuela 681 – Frente al Metro de Alfonso Ugarte','935145632','Luciano Lopez');
INSERT INTO SEDE VALUES(2 , 'PERU','AREQUIPA','Av. Túpac Amaru 6139 – A media cuadra de la Av. Belaunde, por supermercado Vega','936589652','Fernando Contreras');
INSERT INTO SEDE VALUES(3 , 'ECUADOR','QUITO','Av. de los Héroes 736 – A una cuadra de la Estación San Juan','936520123','Maria Lucia Fernandez Alvarez');

CREATE OR REPLACE PACKAGE PK_CLIENTE
IS
    PROCEDURE SP_ListarClientes(TABLA OUT SYS_REFCURSOR);
    PROCEDURE SP_BuscarPorRucCliente(TABLA OUT SYS_REFCURSOR , cRuc IN Cliente.ruc%Type);
    PROCEDURE SP_Mant_Cliente(cRuc IN Cliente.ruc%Type ,
                            cRazonSocial IN Cliente.razon_social%Type ,
                            cTelefono IN Cliente.telefono%Type ,
                            cContacto IN Cliente.contacto%Type ,
                            nAccion INT,
                            nResultado OUT INT);                   
END;


CREATE OR REPLACE PACKAGE BODY PK_CLIENTE
IS

    PROCEDURE SP_ListarClientes
    (
    TABLA OUT SYS_REFCURSOR
    )
    AS
    BEGIN
       OPEN TABLA FOR
       SELECT * FROM Cliente;
    END SP_ListarClientes;

    PROCEDURE SP_BuscarPorRucCliente
    (
    TABLA OUT SYS_REFCURSOR,
    cRuc IN Cliente.ruc%Type
    )
    AS
    BEGIN
       OPEN TABLA FOR
       SELECT * FROM Cliente where ruc = cRuc;
    END SP_BuscarPorRucCliente;
    
    PROCEDURE SP_Mant_Cliente
    (
    cRuc IN Cliente.ruc%Type ,
    cRazonSocial IN Cliente.razon_social%Type ,
    cTelefono IN Cliente.telefono%Type ,
    cContacto IN Cliente.contacto%Type ,
    nAccion INT,
    nResultado OUT INT
    )
    AS
    nId INT;
    BEGIN
       IF nAccion = 1 THEN
         INSERT INTO Cliente(ruc,razon_social,telefono,contacto) VALUES(cRuc ,cRazonSocial ,cTelefono,cContacto);
       END IF;
    
        IF nAccion = 2 THEN
         UPDATE Cliente SET 
             razon_social = cRazonSocial ,
             telefono = cTelefono ,
             contacto = cContacto 
         WHERE ruc = cRuc; 
        END IF;
        
         IF nAccion = 3 THEN
             DELETE FROM Sede_x_Cliente WHERE ruc = cRuc;
             DELETE FROM Cliente WHERE ruc = cRuc; 
         END IF;
         
         nResultado := sql%rowcount;
    END SP_Mant_Cliente;
END;


CREATE OR REPLACE PACKAGE PK_SEDE
IS
    PROCEDURE SP_ListarSedes(TABLA OUT SYS_REFCURSOR);
    PROCEDURE SP_BuscarPorIdSede(TABLA OUT SYS_REFCURSOR , nId INTEGER);
    PROCEDURE SP_Mant_Sede(nid_sede IN Sede.id_sede%Type ,
                cPais IN Sede.pais%Type ,
                cDepartamento IN Sede.departamento%Type ,
                cDireccion IN Sede.direccion%Type ,
                cTelefono IN Sede.telefono%Type ,
                cContacto IN Sede.contacto%Type ,
                nAccion INT,
                nResultado OUT INT); 
    PROCEDURE SP_ListarSedeOrd(TABLA OUT SYS_REFCURSOR);
END;

CREATE OR REPLACE PACKAGE BODY PK_SEDE
IS
    PROCEDURE SP_ListarSedes
    (
    TABLA OUT SYS_REFCURSOR
    )
    AS
    BEGIN
       OPEN TABLA FOR
       SELECT * FROM Sede;
    END;
    
    PROCEDURE SP_BuscarPorIdSede
    (
    TABLA OUT SYS_REFCURSOR,
    nId INTEGER
    )
    AS
    BEGIN
       OPEN TABLA FOR
       SELECT * FROM Sede where id_sede = nId;
    END;
    
    PROCEDURE SP_Mant_Sede
    (
    nid_sede IN Sede.id_sede%Type ,
    cPais IN Sede.pais%Type ,
    cDepartamento IN Sede.departamento%Type ,
    cDireccion IN Sede.direccion%Type ,
    cTelefono IN Sede.telefono%Type ,
    cContacto IN Sede.contacto%Type ,
    nAccion INT,
    nResultado OUT INT
    )
    AS
    nId INT;
    BEGIN
    
       IF nAccion = 1 THEN
         SELECT COUNT(1) INTO nResultado FROM SEDE WHERE UPPER(pais) = UPPER(cPais) AND UPPER(departamento) = UPPER(cDepartamento);
         
         IF nResultado > 0 THEN
            nResultado := -1;
            RETURN;
         END IF;
         SELECT NVL(MAX(id_sede),0) + 1 INTO nId FROM Sede;
         INSERT INTO SEDE(id_sede,pais,departamento,direccion,telefono,contacto) VALUES(nId ,cPais ,cDepartamento,cDireccion , cTelefono , cContacto);
       --  COMMIT;
       END IF;
    
       IF nAccion = 2 THEN
        SELECT COUNT(1) INTO nResultado FROM SEDE WHERE UPPER(pais) = UPPER(cPais) AND UPPER(departamento) = UPPER(cDepartamento)
        AND (id_sede != nid_sede);
         
         IF nResultado > 0 THEN
            nResultado := -1;
            RETURN;
         END IF;
        
         UPDATE Sede SET 
             pais = cPais ,
             departamento = cDepartamento , 
             direccion = cDireccion ,
             telefono = cTelefono ,
             contacto = cContacto 
         WHERE id_sede = nid_sede; 
       --  COMMIT;
        END IF;
        
         IF nAccion = 3 THEN
             DELETE FROM Sede WHERE id_sede = nid_sede; 
             --COMMIT;
         END IF;
         
         nResultado := sql%rowcount;
    END;
    
    PROCEDURE SP_ListarSedeOrd
    (
    TABLA OUT SYS_REFCURSOR
    )
    AS
    BEGIN
       OPEN TABLA FOR
       SELECT id_sede , pais || ' - ' || departamento as "pais" FROM Sede order by pais asc , departamento asc;
    END;
END;




CREATE OR REPLACE PACKAGE PK_SEDExCliente
IS
    PROCEDURE SP_ListarSedeCliente(TABLA OUT SYS_REFCURSOR ,cRuc IN Cliente.ruc%Type);
    PROCEDURE SP_ConsultaFiltroClienteSede(TABLA OUT SYS_REFCURSOR ,cValor IN Sede.pais%Type ,nAccion INT);
    PROCEDURE SP_Mant_SedeCliente(nId_sedeCli IN Sede_x_Cliente.id_sede_cliente%Type ,
                nId_sede IN Sede.id_sede%Type ,
                cRuc IN Cliente.ruc%Type ,
                nAccion INT,
                nResultado OUT INT); 
END;

CREATE OR REPLACE PACKAGE BODY PK_SEDExCliente
IS
    PROCEDURE SP_ListarSedeCliente
    (
    TABLA OUT SYS_REFCURSOR ,
    cRuc IN Cliente.ruc%Type
    )
    AS
    BEGIN
       OPEN TABLA FOR
        select s.pais , s.departamento ,c.ruc , c.razon_social
        from Sede_x_Cliente sc 
        inner join cliente c on 
        c.ruc = sc.ruc
        inner join sede s
        on s.id_sede = sc.id_sede
        where TRIM(sc.ruc) = TRIM(cRuc)
        order by s.pais asc, s.departamento asc , c.ruc asc ;
    END;
    
    PROCEDURE SP_ConsultaFiltroClienteSede
    (
    TABLA OUT SYS_REFCURSOR ,
    cValor IN Sede.pais%Type ,
    nAccion INT
    )
    AS
    BEGIN
       IF nAccion = 1 THEN
            OPEN TABLA FOR
            select s.pais , s.departamento ,c.ruc , c.razon_social , c.telefono , c.contacto
            from Sede_x_Cliente sc 
            inner join cliente c on 
            c.ruc = sc.ruc
            inner join sede s
            on s.id_sede = sc.id_sede
            where UPPER(TRIM(c.ruc)) = UPPER(TRIM(cValor))
            order by s.pais asc, s.departamento asc , c.ruc asc ;
       END IF;
       
       IF nAccion = 2 THEN
            OPEN TABLA FOR
            select s.pais , s.departamento ,c.ruc , c.razon_social , c.telefono , c.contacto
            from Sede_x_Cliente sc 
            inner join cliente c on 
            c.ruc = sc.ruc
            inner join sede s
            on s.id_sede = sc.id_sede
            where UPPER(TRIM(c.razon_social)) = UPPER(TRIM(cValor))
            order by s.pais asc, s.departamento asc , c.ruc asc ;
       END IF;
       
        IF nAccion = 3 THEN
            OPEN TABLA FOR
            select s.pais , s.departamento ,c.ruc , c.razon_social , c.telefono , c.contacto
            from Sede_x_Cliente sc 
            inner join cliente c on 
            c.ruc = sc.ruc
            inner join sede s
            on s.id_sede = sc.id_sede
            where UPPER(TRIM(s.pais)) = UPPER(TRIM(cValor))
            order by s.pais asc, s.departamento asc , c.ruc asc ;
       END IF;
    END;
    
    PROCEDURE SP_Mant_SedeCliente
    (
    nId_sedeCli IN Sede_x_Cliente.id_sede_cliente%Type ,
    nId_sede IN Sede.id_sede%Type ,
    cRuc IN Cliente.ruc%Type ,
    nAccion INT,
    nResultado OUT INT
    )
    AS
    nId INT;
    BEGIN
      IF nAccion = 1 THEN -- VALIDAR PAIS Y DEPARTAMENTO
         
         SELECT COUNT(1) INTO nResultado FROM Sede_x_Cliente WHERE id_sede = nId_sede AND ruc = cRuc;
         
         IF nResultado > 0 THEN
            nResultado := -1;
            RETURN;
         ELSE
            SELECT NVL(MAX(id_sede_cliente),0) + 1 INTO nId FROM Sede_x_Cliente;
            INSERT INTO Sede_x_Cliente(id_sede_cliente,id_sede,ruc) VALUES(nId,nId_sede,cRuc);
         END IF;
       END IF;
    
        nResultado := sql%rowcount;
    END;

END;



commit;