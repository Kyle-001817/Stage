public IActionResult Materiaux(string idBdq, string searchTerm)
    {
        if (!string.IsNullOrEmpty(idBdq))
        {
            HttpContext.Session.SetString("id_bdq", idBdq);
        }
        else
        {
            idBdq = HttpContext.Session.GetString("id_bdq");
        }

        ViewData["type_materiaux"] = _context.TypeMateriel.ToList();

        // Récupérer les matériaux filtrés par idBdq
        List<Materiel> typeMat = Materiel.GetListTypeMaterielExistant(_context, idBdq);
        ViewData["type_materiauxbyBdq"] = typeMat;

        // Appliquer la recherche sur les matériaux filtrés
        var materiauxByType = new Dictionary<string, List<Materiel>>();
        foreach (var type in typeMat)
        {
            string idTypeMateriel = type.IdTypeMateriel;

            // Rechercher parmi les matériaux déjà filtrés par idBdq
            var materiauxQuery = Materiel.Search(_context, searchTerm)
                .Where(m => m.IdBdq == idBdq && m.IdTypeMateriel == idTypeMateriel)
                .ToList();

            materiauxByType[idTypeMateriel] = materiauxQuery;
        }
        ViewData["materiaux_byBdq"] = materiauxByType;

        // Conserver le terme de recherche pour la vue
        ViewData["searchTerm"] = searchTerm;

        if (TempData["ShowSecondModal"] != null && (bool)TempData["ShowSecondModal"])
        {
            ViewBag.ShowSecondModal = true;
        }

        return View();
    }