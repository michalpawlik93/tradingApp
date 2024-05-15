module.exports = {
  env: { browser: true, es2020: true },
  extends: [
    "eslint:recommended",
    "plugin:@typescript-eslint/recommended",
    "plugin:react-hooks/recommended",
  ],
  parser: "@typescript-eslint/parser",
  parserOptions: { ecmaVersion: "latest", sourceType: "module" },
  plugins: ["react-refresh"],
  rules: {
    "react-refresh/only-export-components": "warn",
    "simple-import-sort/imports": [
      "warn",
      {
        groups: [
          ["^react$", "^@?\\w", "^src/", "^\\.\\.(?!/?$)", "^\\.(?!/?$)", "^\\./?$"],
          ["^\\u0000"],
        ],
      },
    ],
    "no-restricted-syntax": [
      "error",
      {
        selector: "ForInStatement",
        message:
          "for..in loops iterate over the entire prototype chain, which is virtually never what you want. Use Object.{keys,values,entries}, and iterate over the resulting array.",
      },
      {
        selector: "LabeledStatement",
        message:
          "Labels are a form of GOTO; using them makes code confusing and hard to maintain and understand.",
      },
      {
        selector: "WithStatement",
        message:
          "`with` is disallowed in strict mode because it makes code impossible to predict and optimize.",
      },
    ],
    eqeqeq: ["error", "smart"],
    "import/no-default-export": "error",
    curly: "warn",
    "prettier/prettier": "warn",
    "import/order": "warn",
    "arrow-body-style": "warn",
    "no-debugger": "warn",
    "spaced-comment": "warn",
    "no-console": ["warn", { allow: ["debug", "error"] }],
  },
};
