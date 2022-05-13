entity Books {
  key ID: Integer;
  title: String;
  author_ID: Integer;
}

entity Authors {
  key Id: Integer;
  firstName: String;
  lastName: String;
}
