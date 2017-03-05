import math
import itertools


def normalize_matrix(matrix):
    sum_matrix = [sum(i) for i in zip(*matrix)]
    result = []

    for row in matrix:
        row = [float(el) / sum_matrix[idx] for idx, el in enumerate(row)]
        result.append(row)

    return result


def get_matrix_enthropy(matrix):
    res = []
    for column in zip(*matrix):
        total = sum(el * math.log(el) for el in column)
        res.append(-1 / math.log(len(matrix)) * total)

    return res


def get_complex_importance(importance, inverted_enphropy):
    sumx = sum([i * e for i, e in zip(importance, inverted_enphropy)])

    return [i * e / sumx for i, e in zip(importance, inverted_enphropy)]


def compare_alternatives(major, minor, criteria):
    return [(mj > mi) == cr for mj, mi, cr in zip(major, minor, criteria)]


def get_permutation_weight(permutation, criteria, importance_complex):
    combinations = list(itertools.combinations(permutation, 2))
    comparisons = [compare_alternatives(i[0], i[1], criteria)
                   for i in combinations]

    weight = 0
    for idx, column in enumerate(zip(*comparisons)):
        for i in column:
            if i:
                weight += importance_complex[idx]
            else:
                weight -= importance_complex[idx]

    return weight


def get_best_permutation(matrix, criteria, importance):
    matrix_normalized = normalize_matrix(matrix)

    enthropy = get_matrix_enthropy(matrix_normalized)
    enthropy_inverted = [1 - i for i in enthropy]
    importance_complex = get_complex_importance(importance, enthropy_inverted)

    matrix_permutations = list(itertools.permutations(matrix))
    weights = [get_permutation_weight(p, criteria, importance_complex)
               for p in matrix_permutations]

    return matrix_permutations[weights.index(max(weights))]


matrix = [[86, 61, 89, 34],
          [94, 63, 95, 31],
          [87, 61, 84, 30],
          [87, 62, 86, 28]]

''' Major is True, minor is False '''
criteria = (True, False, True, False)

importance = [6, 7, 5, 9]

print get_best_permutation(matrix, criteria, importance)
